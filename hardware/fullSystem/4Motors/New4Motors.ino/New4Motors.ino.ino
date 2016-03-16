#include <AFMotor.h>

AF_DCMotor Motor1(1, MOTOR12_64KHZ); // create motor #1, 64KHz pwm
AF_DCMotor Motor2(2, MOTOR12_64KHZ); // create motor #1, 64KHz pwm
AF_DCMotor Motor3(3, MOTOR12_64KHZ); // create motor #1, 64KHz pwm
AF_DCMotor Motor4(4, MOTOR12_64KHZ); // create motor #1, 64KHz pwm

void setup() {
  Serial.begin(250000);           
  Motor1.setSpeed(255);     // set the speed to 255/255
  Motor2.setSpeed(255);     // set the speed to 255/255
  Motor3.setSpeed(255);     // set the speed to 255/255
  Motor4.setSpeed(255);     // set the speed to 255/255
   pinMode(A4, OUTPUT); 
}
 
void loop() {
    digitalWrite(A4,LOW); 
    Motor1.run(FORWARD);
    delay(500);
    Motor1.run(RELEASE);
    delay(50);
    Serial.println(1);
    delay(50);
}






