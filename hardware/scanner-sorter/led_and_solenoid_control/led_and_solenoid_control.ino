#include <Adafruit_NeoPixel.h>
#ifdef __AVR__
#include <avr/power.h>
#endif

#define SOLENOID_PIN       2
#define TOP_CONVEYOR_PIN       A4
#define BOTTOM_CONVEYOR_PIN       A5
#define LED_PIN            6
//LED_PIN8 is just because I did not want to solder...
#define LED_PIN8           5


// How many NeoPixels are attached to the Arduino?
#define NUMPIXELS      26

Adafruit_NeoPixel pixels = Adafruit_NeoPixel(NUMPIXELS, LED_PIN, NEO_GRB + NEO_KHZ800);
Adafruit_NeoPixel pixels8 = Adafruit_NeoPixel(8, LED_PIN8, NEO_GRB + NEO_KHZ800);

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
  pinMode(TOP_CONVEYOR_PIN, OUTPUT);
  pinMode(BOTTOM_CONVEYOR_PIN, OUTPUT);
  digitalWrite(SOLENOID_PIN, HIGH);
  digitalWrite(TOP_CONVEYOR_PIN, LOW);
  digitalWrite(BOTTOM_CONVEYOR_PIN, LOW);
  pixels.begin(); // This initializes the NeoPixel library.
  pixels.setPixelColor(0, pixels.Color(255,255,255));
  pixels.show();
  pixels8.begin(); // This initializes the NeoPixel library.
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
      if (input > 17) {
        pixels8.setPixelColor(old_pixel-18, pixels8.Color(0,0,0));
        pixels8.show();
        pixels8.setPixelColor(input-18, pixels8.Color(255,255,255));
        pixels8.show();
      }
      old_pixel = input;
    } 
    if (input == 26) {
      pixels.setPixelColor(old_pixel, pixels.Color(0,0,0));
      pixels.show();
      for(int i=0;i < 18;i++){
      pixels.setPixelColor(i, pixels.Color(29,29,29));
      pixels.show();
      }
    } 
    if (input == 27) {
      for(int i=0;i < 18;i++){
      pixels.setPixelColor(i, pixels.Color(0,0,0));
      pixels.show();
      }
      for(int i=18;i < NUMPIXELS;i++){
      pixels.setPixelColor(i, pixels.Color(50,50,50));
      pixels.show();
      pixels8.setPixelColor(i-18, pixels8.Color(50,50,50));
      pixels8.show();
      }
    } 
    if (input == 28) {
      for(int i=18;i < NUMPIXELS;i++){
      pixels.setPixelColor(i, pixels.Color(0,0,0));
      pixels.show();
      pixels8.setPixelColor(i-18, pixels8.Color(0,0,0));
      pixels8.show();
      }
     
      pixels.setPixelColor(0, pixels.Color(255,255,255));
      pixels.show();
    } 
    if (input==100) {
      digitalWrite(SOLENOID_PIN, LOW);
    } 
    if (input==101) {
      digitalWrite(SOLENOID_PIN, HIGH);
    } 
    if (input==102) {
      digitalWrite(TOP_CONVEYOR_PIN, LOW);
    } 
    if (input==103) {
      digitalWrite(TOP_CONVEYOR_PIN, HIGH);
    } 
    if (input==102) {
      digitalWrite(BOTTOM_CONVEYOR_PIN, LOW);
    } 
    if (input==103) {
      digitalWrite(BOTTOM_CONVEYOR_PIN, HIGH);
    } 
    if (Serial.read() == '\n') {};
  }
}

