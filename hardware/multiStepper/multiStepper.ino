// MultiStepper
// -*- mode: C++ -*-
//
// Control both Stepper motors at the same time with different speeds
// and accelerations. 
// Requires the AFMotor library (https://github.com/adafruit/Adafruit-Motor-Shield-library)
// And AccelStepper with AFMotor support (https://github.com/adafruit/AccelStepper)
// Public domain!

#include <AccelStepper.h>
#include <AFMotor.h>

int motor_speed = 350;
// two stepper motors one on each port
AF_Stepper motor1(48, 1);
AF_Stepper motor2(48, 2);

// you can change these to DOUBLE or INTERLEAVE or MICROSTEP!
// wrappers for the first motor!
void forwardstep1() {  
  motor1.onestep(FORWARD, DOUBLE);
}
void backwardstep1() {  
  // This is called with a speed of zero
  // There is no stop function in AccelStepper 
  //So this works great for stop if you only need on direction
  //  motor1.onestep(BACKWARD, DOUBLE);
}
// wrappers for the second motor!
void forwardstep2() {  
  motor2.onestep(FORWARD, DOUBLE);
}
void backwardstep2() {  
  // This is called with a speed of zero as there is no stop function
  // There is no stop function in AccelStepper 
  //So this works great for stop if you only need on direction
  //motor2.onestep(BACKWARD, DOUBLE);
}

// Motor shield has two motor ports, now we'll wrap them in an AccelStepper object
AccelStepper stepper1(forwardstep1, backwardstep1);
AccelStepper stepper2(forwardstep2, backwardstep2);

void setup()
{  
  pinMode(A5, INPUT);
  pinMode(A4, INPUT);
  stepper1.setSpeed(motor_speed);
  stepper1.runSpeed();
  stepper2.setSpeed(motor_speed);
  stepper2.runSpeed();
}

void loop()
{
  int run1 = 0;
  run1 = digitalRead(A5);

  int run2 = 0;
  run2 = digitalRead(A4);

  if (run1 == LOW)  {
      stepper1.setSpeed(motor_speed);
      stepper1.runSpeed();

    }
    else
    {
      stepper1.setSpeed(0);
      stepper1.runSpeed();
    }
      
    if (run2 == LOW) {
      stepper2.setSpeed(motor_speed);
      stepper2.runSpeed();
    }
    else
    {
      stepper2.setSpeed(0);
      stepper2.runSpeed();

    }
}
