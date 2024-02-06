#pragma once
#include "Point.h"

class InputListener {

public:
	InputListener() 
	{
	}
	~InputListener() 
	{
	}

	virtual void onKeyDown(int key) = 0;
	virtual void onKeyUp(int key) = 0;

	virtual void onMouseMove(const Point& delta_mouse_pos) = 0;



};
