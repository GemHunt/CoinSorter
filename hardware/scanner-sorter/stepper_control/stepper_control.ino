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
  pinMode(A5, INPUT);
  motor.setSpeed(300);  // rpm
}

void loop() {
  int run1 = 0;
  run1 = digitalRead(A5);


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
  //Serial.println("Micrsostep steps");
  if (run1 == LOW) {
    motor.step(1, FORWARD, MICROSTEP); 
  }
  //motor.step(110, BACKWARD, MICROSTEP); 
  //  delay(100);
}


