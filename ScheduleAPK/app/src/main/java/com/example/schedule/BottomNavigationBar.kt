// BottomNavigationBar.kt
package com.example.schedule

import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.CalendarToday
import androidx.compose.material.icons.filled.Home
import androidx.compose.material.icons.filled.Person
import androidx.compose.material3.*
import androidx.compose.runtime.Composable

@Composable
fun BottomNavigationBar(
    selectedTab: Int,
    onTabSelected: (Int) -> Unit
) {
    NavigationBar {
        NavigationBarItem(
            icon = { Icon(Icons.Default.Home, contentDescription = "Главная") },
            label = { Text("Главная") },
            selected = selectedTab == 0,
            onClick = { onTabSelected(0) }
        )
        NavigationBarItem(
            icon = { Icon(Icons.Default.CalendarToday, contentDescription = "Календарь") },
            label = { Text("Календарь") },
            selected = selectedTab == 1,
            onClick = { onTabSelected(1) }
        )
        NavigationBarItem(
            icon = { Icon(Icons.Default.Person, contentDescription = "Профиль") },
            label = { Text("Профиль") },
            selected = selectedTab == 2,
            onClick = { onTabSelected(2) }
        )
        // Новый элемент для общего расписания
        NavigationBarItem(
            icon = { Icon(Icons.Default.CalendarToday, contentDescription = "Общее расписание") },
            label = { Text("Расписание") },
            selected = selectedTab == 3,
            onClick = { onTabSelected(3) }
        )
    }
}
