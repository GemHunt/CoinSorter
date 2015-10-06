int sensorPin = A0;    // select the input pin for the potentiometer
int sensorValue = 0;  // variable to store the value coming from the sensor
int offCount = 0;

void setup() {
  
  //Initialize serial and wait for port to open:
  Serial.begin(250000);
  while (!Serial) {
    ; // wait for serial port to connect. Needed for Leonardo only
  }
  
}

void loop() {
   //Serial.println(analogRead(sensorPin));
  
  //What does 900 mean? This needs somethings that adjusts
  // for the average power of the sensor through 
  // an ever more dirty belt. 
  // Heck this is the dirty belt sensor!
  if (analogRead(sensorPin) > 900)  {
      offCount = offCount + 1;
    }
  else
  {
  if (offCount != 0) {
      //if it's under 50 it's just a bounce. 
      if (offCount > 50) {
        Serial.println(offCount );
      }
      offCount = 0;
      }
  } 
   

     
     
}
