package com.example.schedule.ui.theme

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.*
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import com.example.schedule.models.ScheduleItem

@Composable
fun TodaySchedule(schedule: List<ScheduleItem>) {
    Column(modifier = Modifier.padding(16.dp)) {
        Text("Schedule for Today", style = MaterialTheme.typography.labelLarge)
        Spacer(modifier = Modifier.height(16.dp))

        LazyColumn {
            items(schedule) { item ->
                ScheduleRow(scheduleItem = item)
            }
        }
    }
}

@Composable
fun ScheduleRow(scheduleItem: ScheduleItem) {
    Row(
        modifier = Modifier.fillMaxWidth().padding(8.dp),
        horizontalArrangement = Arrangement.SpaceBetween
    ) {
        Text(scheduleItem.time)
        Spacer(modifier = Modifier.width(16.dp))
        Text(scheduleItem.subject)
    }
}
