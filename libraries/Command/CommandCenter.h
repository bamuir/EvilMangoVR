#ifndef _COMMAND_CENTER_H
#define _COMMAND_CENTER_H
#include "../HashedString/HashedString.h"

#define NumberOfCommands 6
#define SizeOfCommands    20

struct controllerConfigurations
{
	char Commands[NumberOfCommands][SizeOfCommands];
	char Encodes[NumberOfCommands][SizeOfCommands];
	unsigned int HashedEncodes[NumberOfCommands];
	int sizeOfcommands;
};





struct InputDeviceMessage
{
	char CommandDirection[NumberOfCommands][SizeOfCommands];
	unsigned int HashedCommandDirection[NumberOfCommands];
	float value[NumberOfCommands];
	bool  dataAvailable;
	int index;

	void copyValues(char i_CommandDirection[], float i_value){

		if (index >= NumberOfCommands) return; // out of index 
		dataAvailable = true;

		strcpy(CommandDirection[index], i_CommandDirection);

		HashedCommandDirection [index]= Engine::HashedString::Hash(i_CommandDirection);
		value[index] = i_value;
		index = index + 1;
	}

	void Rest(){
		index = 0;
		dataAvailable = false;
	}

	InputDeviceMessage(){
		index = 0;
		dataAvailable = false;

	}
};













#endif 