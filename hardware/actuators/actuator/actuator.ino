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
}
 
void loop() {
   readIRSensor();
   serialEvent(); 
 if (stringComplete) {
    toggle(true);
    toggleCountdown = 2000;
    inputString = "";
    stringComplete = false;
  }
  if (toggleCountdown != 0) {
    if (toggleCountdown == 1) {
      toggleCountdown = 0;
      toggle(false);
    }
  else {toggleCountdown--;}
  }
}

void readIRSensor() {
  //Serial.println(analogRead(sensorPin));
  
  int irSensorVoltageCutoff = 750;
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
        Serial.println(offCount );
      }
      offCount = 0;
      }
  } 
} 

void serialEvent() {
  while (Serial.available()) {
    // get the new byte:
    char inChar = (char)Serial.read();
    // add it to the inputString:
    inputString += inChar;
    // if the incoming character is a newline, set a flag
    // so the main loop can do something about it:
    if (inChar == '\n') {
      stringComplete = true;
    }
  }
}

void toggle(boolean on) {
  if (on) {
    motor1.run(FORWARD);      // turn it on going forward
    motor2.run(FORWARD);      // turn it on going forward
  }
  else {
    motor1.run(RELEASE);      // stopped
    motor2.run(RELEASE);      // stopped  
   }
}





