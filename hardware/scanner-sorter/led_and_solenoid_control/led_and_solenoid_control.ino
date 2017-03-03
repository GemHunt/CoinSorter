#include <Adafruit_NeoPixel.h>
#ifdef __AVR__
#include <avr/power.h>
#endif

#define SOLENOID_PIN       2
#define LED_PIN            6
#define CONVEYOR_PIN       A5


// How many NeoPixels are attached to the Arduino?
#define NUMPIXELS      18

Adafruit_NeoPixel pixels = Adafruit_NeoPixel(NUMPIXELS, LED_PIN, NEO_GRB + NEO_KHZ800);

int delayval = 65; // delay for half a second
int old_pixel = 0;

void setup() {
  // This is for Trinket 5V 16MHz, you can remove these three lines if you are not using a Trinket
#if defined (__AVR_ATtiny85__)
  if (F_CPU == 16000000) clock_prescale_set(clock_div_1);
#endif
  // End of trinket special code
  Serial.begin(115200);
  pinMode(SOLENOID_PIN, OUTPUT);
  pinMode(CONVEYOR_PIN, OUTPUT);
  digitalWrite(SOLENOID_PIN, HIGH);
  digitalWrite(CONVEYOR_PIN, LOW);
  pixels.begin(); // This initializes the NeoPixel library.
  pixels.setPixelColor(0, pixels.Color(255,255,255));
  pixels.show();
}

void loop() {
  if (Serial.available() > 0) {
    int input = Serial.parseInt();
    if (input < NUMPIXELS) {
      //for(int i=0;i < NUMPIXELS;i++){
      pixels.setPixelColor(old_pixel, pixels.Color(0,0,0));
      pixels.show();
      pixels.setPixelColor(input, pixels.Color(255,255,255));
      pixels.show();
      old_pixel = input;
    } 
    if (input==100) {
      digitalWrite(SOLENOID_PIN, LOW);
    } 
    if (input==101) {
      digitalWrite(SOLENOID_PIN, HIGH);
    } 
    if (input==102) {
      digitalWrite(CONVEYOR_PIN, LOW);
    } 
    if (input==103) {
      digitalWrite(CONVEYOR_PIN, HIGH);
    } 
    if (Serial.read() == '\n') {};
  }
}

