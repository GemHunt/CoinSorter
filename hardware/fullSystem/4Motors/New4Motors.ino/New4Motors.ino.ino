#include <AFMotor.h>

AF_DCMotor motor1(1, MOTOR12_64KHZ); // create motor #1, 64KHz pwm
AF_DCMotor motor2(2, MOTOR12_64KHZ); // create motor #2, 64KHz pwm
AF_DCMotor motor3(3, MOTOR12_64KHZ); // create motor #2, 64KHz pwm
AF_DCMotor motor4(4, MOTOR12_64KHZ); // create motor #2, 64KHz pwm

void setup() {
  Serial.begin(250000);           
  motor1.setSpeed(255);     // set the speed to 255/255
  motor2.setSpeed(255);     // set the speed to 200/255
  motor3.setSpeed(255);     // set the speed to 200/255
  motor4.setSpeed(255);     // set the speed to 200/255
}
 
void loop() {
    motor4.run(RELEASE);
    motor1.run(FORWARD);      
    delay(100); 
    motor1.run(RELEASE);
    motor2.run(FORWARD);      
    delay(100); 
    motor2.run(RELEASE);
    motor3.run(FORWARD);      
    delay(100); 
    motor3.run(RELEASE);
    motor4.run(FORWARD);      
    delay(100); 
}






