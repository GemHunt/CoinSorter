#include <AFMotor.h>

//Version that works for unipolor motors, but I wounder if there is a function for this as well. 
AF_DCMotor motor1(1, MOTOR12_64KHZ); // create motor #1, 64KHz pwm
AF_DCMotor motor2(2, MOTOR12_64KHZ); // create motor #2, 64KHz pwm
AF_DCMotor motor3(3, MOTOR12_64KHZ); // create motor #2, 64KHz pwm
AF_DCMotor motor4(4, MOTOR12_64KHZ); // create motor #2, 64KHz pwm
int d;

void setup() {
  Serial.begin(250000);
  motor1.setSpeed(255);     
  motor2.setSpeed(255);     
  motor3.setSpeed(255);     
  motor4.setSpeed(255);     
  d = 4;
}

void loop() {
 
  motor1.run(FORWARD);
  delay(d);
  motor4.run(RELEASE);
  delay(d);
  motor2.run(FORWARD);
  delay(d);
  motor1.run(RELEASE);
  delay(d);
  motor3.run(FORWARD);
  delay(d);
  motor2.run(RELEASE);
  delay(d);
  motor4.run(FORWARD);
  delay(d);
  motor3.run(RELEASE);
  delay(d);
}




