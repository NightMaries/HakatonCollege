package com.example.schedule
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Card
import androidx.compose.material3.CardDefaults
import androidx.compose.material3.CardElevation
import androidx.compose.material3.Divider
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import com.example.schedule.Schedule

@Composable
fun ScheduleRow(schedule: Schedule) {
    Card(
        modifier = Modifier
            .padding(8.dp)
            .fillMaxWidth(),
        elevation = CardDefaults.cardElevation(defaultElevation = 4.dp)
    ) {
        Column(modifier = Modifier.padding(16.dp)) {
            Text(
                text = "Группа: ${schedule.groupName}",
                style = MaterialTheme.typography.labelLarge
            )
            Text(
                text = "Предмет: ${schedule.subjectName ?: "Не указан"}",
                style = MaterialTheme.typography.bodyLarge
            )
            Text(
                text = "Преподаватель: ${schedule.teacherFIO ?: "Не указан"}",
                style = MaterialTheme.typography.bodyLarge
            )
            Text(
                text = "День недели: ${schedule.weekDay}",
                style = MaterialTheme.typography.bodyLarge
            )
            Text(
                text = "Время: ${schedule.scheduleStart} - ${schedule.scheduleEnd}",
                style = MaterialTheme.typography.bodyLarge
            )
            Text(
                text = "Номер пары: ${schedule.scheludeNumber}",
                style = MaterialTheme.typography.bodyLarge
            )
        }
    }
}


