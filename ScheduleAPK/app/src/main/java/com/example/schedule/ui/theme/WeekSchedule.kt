package com.example.schedule.ui.theme

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.LazyRow
import androidx.compose.foundation.lazy.items
import androidx.compose.foundation.text.BasicText
import androidx.compose.material3.*
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import com.example.schedule.models.ScheduleItem

@Composable
fun WeekSchedule(schedule: List<ScheduleItem>, onDaySelected: (String) -> Unit) {
    val daysOfWeek = listOf("Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday")

    Column(modifier = Modifier.padding(16.dp)) {
        Text("Schedule for the Week", style = MaterialTheme.typography.labelLarge)
        Spacer(modifier = Modifier.height(16.dp))

        LazyRow {
            items(daysOfWeek) { day ->
                Button(onClick = { onDaySelected(day) }) {
                    Text(day)
                }
            }
        }

        Spacer(modifier = Modifier.height(16.dp))

        LazyColumn {
            items(schedule) { item ->
                ScheduleRow(scheduleItem = item)
            }
        }
    }
}
