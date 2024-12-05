package com.example.schedule

import android.os.Build
import android.os.Bundle
import androidx.activity.compose.setContent
import androidx.annotation.RequiresApi
import androidx.appcompat.app.AppCompatActivity
import androidx.compose.runtime.*
import com.example.schedule.ui.theme.ScheduleTheme

class MainActivity : AppCompatActivity() {
    @RequiresApi(Build.VERSION_CODES.O)
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {
            ScheduleTheme {
                var token by remember { mutableStateOf("") }
                var roleId by remember { mutableStateOf("") }

                if (token.isEmpty()) {
                    LoginScreen { newToken, newRoleId ->
                        token = newToken
                        roleId = newRoleId
                    }
                } else {
                    MainScreen(token = token)
                }
            }
        }
    }
}
