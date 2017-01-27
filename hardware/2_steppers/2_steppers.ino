// Adafruit Motor shield library
// copyright Adafruit Industries LLC, 2009
// this code is public domain, enjoy!

#include <AFMotor.h>

// Connect a stepper motor with 48 steps per revolution (7.5 degree)
// to motor port #2 (M3 and M4)
AF_Stepper motor0(48, 1);
AF_Stepper motor1(48, 2);
int motor_command =7;

void setup() {
  Serial.begin(9600);
  Serial.println("I am starting up!");
  motor0.setSpeed(1000);  // rpm
  motor1.setSpeed(1000);  // rpm
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
  //Serial.println("Micrsostep steps");
  //motor1.step(8, FORWARD, MICROSTEP); 
  //motor2.step(10, FORWARD, MICROSTEP); 
//  Serial.println(motor_command);
 
  if (Serial.available() > 0) {
    int input = Serial.parseInt();
    if (input > 0) {
    motor_command = input; 
    Serial.println(motor_command + 100);
        Serial.println("-");
    }
    if (Serial.read() == '\n') {};
  }

  if (motor_command & 1) {
    //Serial.println("Motor 0 on");
    if (motor_command & 2) {
      //Serial.println("Motor 0 is Forward");
      motor0.step(1, FORWARD, MICROSTEP); 
    }
    else {
      //Serial.println("Motor 0 is Reverse");
      motor0.step(1, BACKWARD, MICROSTEP); 
    }
  }
  if (motor_command & 4) {
    //Serial.println("Motor 1 on");
    if (motor_command & 8) {
      //Serial.println("Motor 1 is Forward");
      motor1.step(1, FORWARD, MICROSTEP); 
    }
    else {
      //Serial.println("Motor 1 is Reverse");
      motor1.step(1, BACKWARD, MICROSTEP); 
    }
  }


//   delay(300);

  
}
