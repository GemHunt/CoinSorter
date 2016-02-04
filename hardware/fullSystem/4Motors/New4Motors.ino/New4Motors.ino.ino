#include <AFMotor.h>

AF_DCMotor motor1(1, MOTOR12_64KHZ); // create motor #1, 64KHz pwm
AF_DCMotor motor2(2, MOTOR12_64KHZ); // create motor #2, 64KHz pwm
AF_DCMotor motor3(3, MOTOR12_64KHZ); // create motor #2, 64KHz pwm
AF_DCMotor motor4(4, MOTOR12_64KHZ); // create motor #2, 64KHz pwm

void setup() {
  Serial.begin(250000);           
   motor1.setSpeed(255);     // set the speed to 200/255
  motor2.setSpeed(255);     // set the speed to 200/255
  motor3.setSpeed(255);     // set the speed to 200/255
  //motor4.setSpeed(255);     // set the speed to 200/255
   //motor1.run(FORWARD);      // turn it on going forward
    //motor2.run(FORWARD);      // turn it on going forward
}
 
void loop() {
 //for (int i=0; i <= 10; i++){
    motor1.run(FORWARD);      // turn it on going forward
    motor2.run(FORWARD);      // turn it on going forward
    motor3.run(FORWARD);      // turn it on going forward
    //motor4.run(FORWARD);      // turn it on going forward
    delay(50); 
    //motor1.run(BACKWARD);      // turn it on going forward
    //motor1.run(RELEASE);      // turn it on going forward
    //motor2.run(RELEASE);      // turn it on going forward
   //motor3.run(RELEASE);      // turn it on going forward
    delay(50); 
    //} 
//motor1.run(RELEASE); 
//delay(500);

   
}






