
#include <string.h>

#include <SipPuff.h>

SipPuff sip(A0);
//SipPuff sip2(5);

bool JoyStickAvailbe;
void setup() {
  Serial.begin(115200);
  sip.setNominalPressure();
  sip.multiStateTime = 200;
}

void loop() {
  sip.UpdateC();  // call this function for continuous sipping and puffing functions. 
  // sip.update();
  InputDevicesLoop(); // Detecting inputs from the user and map it to usable control scheme.
}

void InputDevicesLoop() {
  if (sip.finalPattern == "S")
    Serial.println("L");
  else if (sip.finalPattern == "P")
    Serial.println("R");
  else if (sip.finalPattern == "PP")
    Serial.println("F");
  else if (sip.finalPattern == "SS")
    Serial.println("B");
  else if (sip.finalPattern == "PSP")
    Serial.println("C1");
  else if (sip.finalPattern == "PSS")
    Serial.println("C2");
  else if (sip.finalPattern == "SPP")
    Serial.println("C3");
  else if (sip.finalPattern == "PPP")
    Serial.println("ESC");
    
  if (sip.c_done)
    Serial.println("U");
}
