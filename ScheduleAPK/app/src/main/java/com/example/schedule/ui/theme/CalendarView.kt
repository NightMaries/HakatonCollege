package com.example.schedule.ui.theme

import androidx.compose.foundation.layout.*
import androidx.compose.material3.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp

@Composable
fun CalendarView(onDateSelected: (String) -> Unit) {
    val selectedDate = remember { mutableStateOf("") }

    Column(modifier = Modifier.padding(16.dp)) {
        Text("Select a Date", style = MaterialTheme.typography.labelLarge)

        Button(onClick = {
            selectedDate.value = "2024-12-03"
            onDateSelected(selectedDate.value)
        }) {
            Text("Pick a date")
        }

        Spacer(modifier = Modifier.height(16.dp))

        Text("Selected Date: ${selectedDate.value}", style = MaterialTheme.typography.bodyLarge)
    }
}
