#include <AFMotor.h>

AF_DCMotor motor1(1, MOTOR12_64KHZ); // create motor #1, 64KHz pwm
AF_DCMotor motor2(2, MOTOR12_64KHZ); // create motor #2, 64KHz pwm
String inputString = "";         // a string to hold incoming data
boolean stringComplete = false;  // whether the string is complete
int toggleCountdown = 0;
int sensorPin = A0;    // select the input pin for the potentiometer
int sensorValue = 0;  // variable to store the value coming from the sensor
int offCount = 0;

void setup() {
  Serial.begin(250000);           
  motor1.setSpeed(255);     // set the speed to 200/255
  motor2.setSpeed(255);     // set the speed to 200/255
  inputString.reserve(10);
  pinMode(A1, OUTPUT);
  digitalWrite(A1, HIGH);
  pinMode(A2, OUTPUT);
  digitalWrite(A2, HIGH);
  pinMode(A3, OUTPUT);
  digitalWrite(A3, HIGH);
  pinMode(A4, OUTPUT);
  digitalWrite(A4, HIGH);
}
 
void loop() {
   readIRSensor();
}

void readIRSensor() {
   Serial.println(analogRead(sensorPin));
  int irSensorVoltageCutoff = 50;
  // the average power of the sensor through an ever more dirty belt. 
  // Heck this is the dirty belt sensor!
  //irSensorVoltageCutoff needs to adjust, maybe it should be set on the PC side?
  if (analogRead(sensorPin) > irSensorVoltageCutoff)  {
      offCount = offCount + 1;
    }
  else
  {
  if (offCount != 0) {
      //if it's under 50 it's just a bounce.
      //if it's over 1800 pennies are stacked
      if ((offCount > 50) && (offCount < 2000)) {
         toggle();
      }
      offCount = 0;
      }
  } 
} 

void toggle() {
     delay(70); 
     digitalWrite(A1, LOW);
    delay(30);  
     digitalWrite(A1, HIGH);
}






