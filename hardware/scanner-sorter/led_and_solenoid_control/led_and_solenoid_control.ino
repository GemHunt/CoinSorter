#include <Adafruit_NeoPixel.h>
#ifdef __AVR__
#include <avr/power.h>
#endif

#define SOLENOID_PIN       2
#define LED_PIN            6

// How many NeoPixels are attached to the Arduino?
#define NUMPIXELS      18

Adafruit_NeoPixel pixels = Adafruit_NeoPixel(NUMPIXELS, LED_PIN, NEO_GRB + NEO_KHZ800);

int delayval = 65; // delay for half a second

void setup() {
  // This is for Trinket 5V 16MHz, you can remove these three lines if you are not using a Trinket
#if defined (__AVR_ATtiny85__)
  if (F_CPU == 16000000) clock_prescale_set(clock_div_1);
#endif
  // End of trinket special code
  Serial.begin(9600);
  pinMode(SOLENOID_PIN, OUTPUT);
  digitalWrite(SOLENOID_PIN, HIGH);
  pixels.begin(); // This initializes the NeoPixel library.
}

void loop() {

  // For a set of NeoPixels the first NeoPixel is 0, second is 1, all the way up to the count of pixels minus one.
  int on_pixel;
  int off_pixel;
  int num_pixels_on = 2;
  //This leaves 2 pixels on at all times:
  for(int i=0;i < NUMPIXELS;i++){
    on_pixel = i;
    off_pixel = i - num_pixels_on;
    if (on_pixel <  0){
      on_pixel += NUMPIXELS;
    }
    if (off_pixel < 0){
      off_pixel += NUMPIXELS;
    }
    if (on_pixel > NUMPIXELS){
      on_pixel -= NUMPIXELS;
    }
    if (off_pixel > NUMPIXELS){
      off_pixel -= NUMPIXELS;
    }
    pixels.setPixelColor(on_pixel, pixels.Color(255,255,255));
    pixels.show();
    pixels.setPixelColor(off_pixel, pixels.Color(0,0,0));
    pixels.show();
    
    if (Serial.available() > 0) {
      int input = Serial.parseInt();
      digitalWrite(SOLENOID_PIN, LOW);
    }
    if (Serial.read() == '\n') {};
    delay(delayval);
    digitalWrite(SOLENOID_PIN, HIGH);
  }
}

