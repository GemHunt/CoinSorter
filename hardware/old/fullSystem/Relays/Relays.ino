void setup() {
  Serial.begin(250000);           
  pinMode(A2, OUTPUT); 
  digitalWrite(A2,LOW); 
  pinMode(A3, OUTPUT); 
  digitalWrite(A3,LOW); 
  pinMode(A4, OUTPUT); 
}
 
void loop() {
    digitalWrite(A4,LOW); 
    delay(150);
    digitalWrite(A4,HIGH); 
    delay(190);
    Serial.println(1);
    delay(40);
}






