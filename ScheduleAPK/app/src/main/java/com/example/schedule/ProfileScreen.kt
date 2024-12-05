package com.example.schedule

import androidx.compose.foundation.layout.*
import androidx.compose.material3.Button
import androidx.compose.material3.Checkbox
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp

@Composable
fun ProfileScreen(token: String) {
    var isSubscribed by remember { mutableStateOf(true) }

    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(16.dp),
        verticalArrangement = Arrangement.Center,
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        Text("Ваш профиль", style = MaterialTheme.typography.headlineSmall)

        Spacer(modifier = Modifier.height(16.dp))

        Text("Информация о пользователе")
        Text("Роль: Студент")

        Spacer(modifier = Modifier.height(16.dp))

        Row(verticalAlignment = Alignment.CenterVertically) {
            Checkbox(
                checked = isSubscribed,
                onCheckedChange = { isSubscribed = it }
            )
            Text("Подписаться на уведомления")
        }

        Spacer(modifier = Modifier.height(16.dp))

        Button(onClick = { /* Реализация смены пароля в будущем */ }) {
            Text("Сменить пароль")
        }
    }
}
