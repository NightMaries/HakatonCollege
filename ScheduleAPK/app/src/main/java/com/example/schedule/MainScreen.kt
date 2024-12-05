package com.example.schedule

import android.os.Build
import androidx.annotation.RequiresApi
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier

@RequiresApi(Build.VERSION_CODES.O)
@Composable
fun MainScreen(token: String) {
    var selectedTab by remember { mutableStateOf(0) }

    Scaffold(
        bottomBar = {
            BottomNavigationBar(
                selectedTab = selectedTab,
                onTabSelected = { selectedTab = it }
            )
        }
    ) { innerPadding ->
        Box(modifier = Modifier.padding(innerPadding)) {
            when (selectedTab) {
                0 -> ScheduleScreen(token = token)  // Главная (расписание на текущий день)
                1 -> CalendarScreen(token = token) // Календарь
                2 -> ProfileScreen(token = token)  // Профиль
                3 -> GeneralScheduleScreenWithData(token)

            }
        }
    }
}
