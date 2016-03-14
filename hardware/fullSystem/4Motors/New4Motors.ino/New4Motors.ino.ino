#include <AFMotor.h>

AF_DCMotor inputMotor(1, MOTOR12_64KHZ); // create motor #1, 64KHz pwm
AF_DCMotor topLights(2, MOTOR12_64KHZ); // create motor #2, 64KHz pwm
AF_DCMotor bottomLights(3, MOTOR12_64KHZ); // create motor #2, 64KHz pwm

void setup() {
  Serial.begin(250000);           
  inputMotor.setSpeed(255);     // set the speed to 255/255
  topLights.setSpeed(25);     // set the speed to 200/255
  bottomLights.setSpeed(40);     // set the speed to 200/255
}
 
void loop() {
    inputMotor.run(FORWARD);
    //topLights.run(FORWARD);
    //bottomLights.run(FORWARD);
    //motor4.run(RELEASE);
    //motor1.run(FORWARD);      
}






