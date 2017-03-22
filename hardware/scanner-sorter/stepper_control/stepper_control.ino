// Adafruit Motor shield library
// copyright Adafruit Industries LLC, 2009
// this code is public domain, enjoy!

#include <AFMotor.h>

// Connect a stepper motor with 48 steps per revolution (7.5 degree)
AF_Stepper motor1(48, 1);
AF_Stepper motor2(48, 2);

void setup() {
  pinMode(A5, INPUT);
  pinMode(A4, INPUT);
  motor1.setSpeed(750);  // rpm
  motor2.setSpeed(750);  // rpm
}

void loop() {
  int run1 = 0;
  run1 = digitalRead(A5);

  int run2 = 0;
  run2 = digitalRead(A4);

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
  
  //for (int x=0;x<2;x++){
    if (run1 == LOW)  {
      motor1.step(1, FORWARD, INTERLEAVE);
    }
    else
    {
      motor1.release();
    }
      
    if (run2 == LOW) {
      motor2.step(1, FORWARD, INTERLEAVE); 
    }
  else
    {
      motor2.release();
    }

  //}
  //motor.step(110, BACKWARD, MICROSTEP); 
  //  delay(100);
}


