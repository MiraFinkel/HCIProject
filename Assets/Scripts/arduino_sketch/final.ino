#include <Wire.h>
#include <MPU6050.h>

MPU6050 mpu;
int ledPins[] = {3,4,5,6};  
const int pinButton = 9;
unsigned int count = 0;


const int MPU_addr=0x68;
int16_t AcX,AcY,AcZ,Tmp,GyX,GyY,GyZ,AccX;
int minVal=265;
int maxVal=402;
double x;
double y;
double z;


float gesture1Convolution;
float gesture2Convolution;
float gesture1Value = 1;
float gesture2Value = 1;
float alpha = 0.5;
int signalLength = 100;
float signalInput[100] = {0};
int i = 0;




float FILTER1[31] = {-0.003979816589957558, -0.0014399200769895143, 0.009501496191536377, -0.016995304935556843, 0.014981838405342186, 
                      0.004220957970882878, -0.03553492193290273, 0.05762234063445485, -0.04568569914077357, -0.006223582107310084, 
                      0.07249652798646813, -0.10797857438265043, 0.08006008582545608, 0.0030805450684704703, -0.09259676363241945, 
                      0.13089158272522738, -0.09259676363241945, 0.0030805450684704703, 0.08006008582545608, -0.10797857438265045, 
                      0.07249652798646815, -0.006223582107310084, -0.04568569914077359, 0.05762234063445488, -0.03553492193290273, 
                      0.004220957970882883, 0.014981838405342198, -0.016995304935556843, 0.009501496191536382, -0.0014399200769895143, 
                      -0.003979816589957558};

float FILTER2[31] = {-0.00227553976299975, -0.0016940987730128742, -0.0009996279955455062, 0.00046135199013865876, 0.0033076056192072787, 
                      0.008028467891491014, 0.014901900900540015, 0.023936935205545153, 0.034849146971722145, 0.04707360185194397, 
                      0.059814777750526404, 0.0721280260212264, 0.08302282423369658, 0.09157501273685976, 0.09703382562730507, 
                      0.09891003027337354, 0.09703382562730507, 0.09157501273685976, 0.08302282423369658, 0.0721280260212264, 
                      0.059814777750526425, 0.04707360185194397, 0.03484914697172215, 0.023936935205545164, 0.014901900900540015, 
                      0.008028467891491024, 0.0033076056192072813, 0.00046135199013865876, -0.0009996279955455066, -0.0016940987730128742, 
                      -0.00227553976299975};
void setup() 
{
    pinMode(ledPins[0],OUTPUT);  // ledPins[0] = 3
  pinMode(ledPins[1],OUTPUT);  // ledPins[1] = 4
  pinMode(ledPins[2],OUTPUT);  // ledPins[2] = 5
  pinMode(ledPins[3],OUTPUT);  // ledPins[3] = 6
  ledsControl();
   pinMode(pinButton, INPUT);
   Wire.begin();
  Wire.beginTransmission(MPU_addr);
  Wire.write(0x6B);
  Wire.write(0);
  Wire.endTransmission(true);
  Serial.begin(9600);

//   Initialize MPU6050
  while(!mpu.begin(MPU6050_SCALE_2000DPS, MPU6050_RANGE_2G))
  {
    delay(500);
  }
  mpu.calibrateGyro();

  // Set threshold sensivty. Default 3.
  mpu.setThreshold(3);  

//   turn on the LEDs
  ledsControl();
}

void loop()
{
  getButton();
  getAngles();
  
  signalInput[i] = (float)AcX;
      convolution();
      runningAverage();
      reactToGestures(gesture1Value, gesture2Value);
      i = (i + 1) % signalLength;
}

void getButton()
{
  int stateButton = digitalRead(pinButton);
  if(stateButton == 0) {
          Serial.print("0,");
  } else {
    Serial.print("1,");
  }
}


void convolution()
{
  gesture1Convolution = 0;
  gesture2Convolution = 0;

  int filterLength = sizeof(FILTER1) / sizeof(FILTER1[0]);

  for (int j = 0; j < filterLength; j++)
  {
    int convolutionIdx = (i - j + signalLength) % signalLength;
    gesture1Convolution += signalInput[convolutionIdx] * FILTER1[j];
    gesture2Convolution += signalInput[convolutionIdx] * FILTER2[j];
  }
}

void runningAverage()
{
  gesture1Value = alpha * abs(gesture1Convolution) + (1.0 - alpha) * gesture1Value;
  gesture2Value = alpha * abs(gesture2Convolution) + (1.0 - alpha) * gesture2Value;
}

void reactToGestures(float gesture1Value, float gesture2Value)
{
  if (gesture1Value > 3000)
  {
    Serial.println("5");
  }
  else if (gesture2Value > 26000)
  {
    if(((x >= 280) && (x <= 330)) && ((z <= 320) && (z >= 270)))
    {
     Serial.println("4"); 
    }
    else if(((x >= 55) && (x <= 150)) && ((y <= 290) && (y >= 220)) && ((z <= 165) && (z >= 130)))
    {
      Serial.println("2");
    }
    else if(((z >= 170) && (z <= 220)) && ((y <= 300) && (y >= 230)))
    {
      Serial.println("3");
    }
  }
  else
    {
      Serial.println("0"); 
    }
}

void getAngles()
{
  Wire.beginTransmission(MPU_addr);
  Wire.write(0x3B);
  Wire.endTransmission(false);
  Wire.requestFrom(MPU_addr,14,true);
  AcX=Wire.read()<<8|Wire.read();
  AcY=Wire.read()<<8|Wire.read();
  AcZ=Wire.read()<<8|Wire.read();
  int xAng = map(AcX,minVal,maxVal,-90,90);
  int yAng = map(AcY,minVal,maxVal,-90,90);
  int zAng = map(AcZ,minVal,maxVal,-90,90);
   
  x= RAD_TO_DEG * (atan2(-yAng, -zAng)+PI);
  y= RAD_TO_DEG * (atan2(-xAng, -zAng)+PI);
  z= RAD_TO_DEG * (atan2(-yAng, -xAng)+PI);
   
  Serial.print(x); Serial.print(",");
  Serial.print(z); Serial.print(",");
  Serial.print(y); Serial.print(",");
}

void ledsControl()
{
  int index;
  for(index = 0; index <= 3; index = ++index)  // step through index from 0 to 7
  {
    digitalWrite(ledPins[index], HIGH);
  }                                        
}
