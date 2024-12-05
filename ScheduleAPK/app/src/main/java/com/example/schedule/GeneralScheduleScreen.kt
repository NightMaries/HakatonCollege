package com.example.schedule

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp

@Composable
fun GeneralScheduleScreen(
    scheduleData: List<Schedule>,
    searchText: String,
    onSearch: (String) -> Unit,
    onSearchTextChange: (String) -> Unit
) {
    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(16.dp)
    ) {
        TextField(
            value = searchText,
            onValueChange = {
                onSearchTextChange(it)
                onSearch(it)
            },
            label = { Text("Поиск по дисциплине, кабинету, группе, преподавателю") },
            modifier = Modifier.fillMaxWidth()
        )

        Spacer(modifier = Modifier.height(16.dp))

        LazyColumn {
            items(scheduleData) { schedule ->
                ScheduleCard(schedule)
            }
        }
    }
}

@Composable
fun ScheduleCard(schedule: Schedule) {
    Card(
        modifier = Modifier
            .fillMaxWidth()
            .padding(vertical = 8.dp),
        elevation = CardDefaults.elevatedCardElevation(defaultElevation = 8.dp)
    ) {
        Column(
            modifier = Modifier
                .fillMaxWidth()
                .padding(16.dp)
        ) {
            Text(
                text = "Расписание на ${schedule.weekDay}",
                style = MaterialTheme.typography.titleMedium,
                modifier = Modifier.padding(bottom = 8.dp)
            )

            Text("Дисциплина: ${schedule.subjectName}")
            Text("Кабинет: ${schedule.scheludeNumber}")
            Text("Группа: ${schedule.groupName}")
            Text("Преподаватель: ${schedule.teacherFIO}")

            Spacer(modifier = Modifier.height(8.dp))
        }
    }
}



