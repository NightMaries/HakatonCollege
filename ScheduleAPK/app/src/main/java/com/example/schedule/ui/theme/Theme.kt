package com.example.schedule.ui.theme

import androidx.compose.material3.MaterialTheme
import androidx.compose.runtime.Composable
import androidx.compose.ui.tooling.preview.Preview
import com.example.schedule.ScheduleScreen



@Composable
fun ScheduleTheme(content: @Composable () -> Unit) {
    MaterialTheme(
        colorScheme = MaterialTheme.colorScheme.copy(
            primary = GreenPrimary, // Зеленый для акцентов
            secondary = GreenSecondary, // Нежно-зеленый
            background = BackgroundWhite, // Белый фон
            surface = SurfaceGray, // Легкий серый для подложки
            onPrimary = OnPrimaryText, // Белый текст на зеленом
            onSecondary = OnSecondaryText, // Темно-серый для текста на светло-зеленом
            onBackground = OnBackgroundText, // Темный текст на светлом фоне
            onSurface = OnSurfaceText // Темный текст на сером фоне
        ),
        content = content
    )
}

@Preview(showBackground = true)
@Composable
fun DefaultPreview() {
    ScheduleTheme {
        // Пример контента с темой
        ScheduleScreen(token = "sampleToken123")
    }
}
