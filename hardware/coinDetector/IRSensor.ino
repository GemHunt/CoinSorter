int sensorPin = A0;    // select the input pin for the potentiometer
int sensorValue = 0;  // variable to store the value coming from the sensor

void setup() {
  
  //Initialize serial and wait for port to open:
  Serial.begin(250000);
  while (!Serial) {
    ; // wait for serial port to connect. Needed for Leonardo only
  }
  // prints title with ending line break
  //Serial.println("Reading PIN 0");
}

void loop() {
     Serial.print((analogRead(sensorPin)-650)/40 );
     Serial.print("\n");
}
