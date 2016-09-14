// Adafruit Motor shield library
// copyright Adafruit Industries LLC, 2009
// this code is public domain, enjoy!

#include <AFMotor.h>

// Connect a stepper motor with 48 steps per revolution (7.5 degree)
// to motor port #2 (M3 and M4)
AF_Stepper motor(48, 1);

void setup() {
  Serial.begin(9600);           // set up Serial library at 9600 bps
  Serial.println("Stepper test!");

  motor.setSpeed(250);  // rpm
}

void loop() {
/*
  Serial.println("Single coil steps");
  motor.step(100, FORWARD, SINGLE); 
  motor.step(100, BACKWARD, SINGLE); 

  Serial.println("Double coil steps");
  motor.step(100, FORWARD, DOUBLE); 
  motor.step(100, BACKWARD, DOUBLE);

  Serial.println("Interleave coil steps");
  motor.step(100, FORWARD, INTERLEAVE); 
  motor.step(100, BACKWARD, INTERLEAVE); 
*/
  Serial.println("Micrsostep steps");
  motor.step(250, FORWARD, MICROSTEP); 
  motor.step(250, BACKWARD, MICROSTEP); 
}




/*
Version that works for unipolor motors, but I wounder if there is a function for this as well. 
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
 
  motor1.run(FORWARD);      // turn it on going forward
  delay(d);
  motor4.run(RELEASE);      // turn it on going forward
  delay(d);
  motor2.run(FORWARD);      // turn it on going forward
  delay(d);
  motor1.run(RELEASE);      // turn it on going forward
  delay(d);
  motor3.run(FORWARD);      // turn it on going forward
  delay(d);
  motor2.run(RELEASE);      // turn it on going forward
  delay(d);
  motor4.run(FORWARD);      // turn it on going forward
  delay(d);
  motor3.run(RELEASE);      // turn it on going forward
  delay(d);
  
  
  /*
  delay(10);
  motor1.run(RELEASE);      // turn it on going forward
  motor2.run(FORWARD);      // turn it on going forward
  delay(10);
  motor1.run(BACKWARD);      // turn it on going forward
  motor2.run(RELEASE);      // turn it on going forward
  delay(10);
  motor1.run(RELEASE);      // turn it on going forward
  motor2.run(BACKWARD);      // turn it on going forward
  delay(10);
  */
}
*/





