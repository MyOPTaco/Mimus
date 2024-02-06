#include "InputSystem.h"
#include <Windows.h>

InputSystem::InputSystem() {

}

InputSystem::~InputSystem() {

}

void InputSystem::update()
{
	POINT current_mouse_pos = {};
	::GetCursorPos(&current_mouse_pos);

	if (current_mouse_pos.x != m_old_mouse_pos.m_x || current_mouse_pos.y != m_old_mouse_pos.m_y)
	{
		std::unordered_set<InputListener*, InputListener*>::iterator it = m_set_listeners.begin();

		while (it != m_set_listeners.end())
		{
			(*it)->onMouseMove(Point(current_mouse_pos.x - m_old_mouse_pos.m_x, current_mouse_pos.y - m_old_mouse_pos.m_y));
			++it;
		}


	}


	if (::GetKeyboardState(m_keys_state)) {

		for (unsigned int i = 0; i < 256; i++) {

			if (m_keys_state[i] & 0x80)
			{
				std::map<InputListener*, InputListener*>::iterator it = m_map_listeners.begin();

				while (it != m_map_listeners.end())
				{
					it->second->onKeyDown(i);
					++it;;
				}
			}
			else 
			{
				if (m_keys_state[i] != m_old_keys_state[i]) {
					{
						std::map<InputListener*, InputListener*>::iterator it = m_map_listeners.begin();

						while (it != m_map_listeners.end())
						{

							it->second->onKeyUp(i);
							++it;;
						}
					}
				}
			}
		}
		::memcpy(m_old_keys_state, m_keys_state, sizeof(unsigned char) * 256);
	}
}

void InputSystem::addListener(InputListener* listener)
{
	m_map_listeners.insert(std::make_pair< InputListener*, InputListener*>
		(std::forward< InputListener*>(listener), std::forward< InputListener*>(listener)));
}

void InputSystem::removeListener(InputListener* listener)
{
	std::map<InputListener*, InputListener*>::iterator it = m_map_listeners.find(listener);

	if (it != m_map_listeners.end()) 
	{
		m_map_listeners.erase(it);
	}
}

InputSystem* InputSystem::get() {
	static InputSystem system;
	return &system;
}