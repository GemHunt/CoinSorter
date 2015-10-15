#include <AFMotor.h>

AF_DCMotor motor1(1, MOTOR12_64KHZ); // create motor #1, 64KHz pwm
AF_DCMotor motor2(2, MOTOR12_64KHZ); // create motor #2, 64KHz pwm
String inputString = "";         // a string to hold incoming data
boolean stringComplete = false;  // whether the string is complete
 
void setup() {
  Serial.begin(250000);           // set up Serial library at 9600 bps
  Serial.println("Motor test!");
  motor1.setSpeed(255);     // set the speed to 200/255
  motor2.setSpeed(255);     // set the speed to 200/255
  inputString.reserve(10);
}
 
void loop() {
  serialEvent(); 
 if (stringComplete) {
    toggle();
    inputString = "";
    stringComplete = false;
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

void toggle() {
  motor1.run(FORWARD);      // turn it on going forward
  motor2.run(FORWARD);      // turn it on going forward
  delay(50);
  motor1.run(RELEASE);      // stopped
  motor2.run(RELEASE);      // stopped
}




