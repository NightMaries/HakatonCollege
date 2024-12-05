package com.example.schedule

import android.os.Build
import android.widget.CalendarView
import android.widget.Toast
import androidx.annotation.RequiresApi
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.viewinterop.AndroidView
import androidx.compose.ui.unit.dp
import com.example.schedule.ApiClient.scheduleService
import kotlinx.coroutines.launch
import java.time.LocalDate
import java.time.format.DateTimeFormatter
import java.util.*

@RequiresApi(Build.VERSION_CODES.O)
@Composable
fun CalendarScreen(token: String) {
    val context = LocalContext.current
    val selectedDate = remember { mutableStateOf(LocalDate.now()) }
    val schedules = remember { mutableStateListOf<Schedule>() }
    val coroutineScope = rememberCoroutineScope()

    Column(modifier = Modifier.fillMaxWidth().padding(16.dp)) {
        AndroidView(
            modifier = Modifier
                .fillMaxWidth()
                .background(Color(0x80000000))
                .padding(8.dp),
            factory = { CalendarView(context) },
            update = { calendarView ->
                calendarView.setOnDateChangeListener { _, year, month, dayOfMonth ->
                    selectedDate.value = LocalDate.of(year, month + 1, dayOfMonth)
                }
            }
        )

        Spacer(modifier = Modifier.height(16.dp))

        Text(
            text = "Расписание на ${selectedDate.value.format(DateTimeFormatter.ofPattern("dd MMMM yyyy", Locale("ru")))}",
            style = MaterialTheme.typography.bodyLarge
        )

        Spacer(modifier = Modifier.height(8.dp))

        LaunchedEffect(selectedDate.value) {
            coroutineScope.launch {
                try {
                    val response = scheduleService.getSchedules(token)
                    if (response.isSuccessful) {
                        schedules.clear()
                        schedules.addAll(response.body()?.filter {
                            it.weekDay == getDayOfWeek(selectedDate.value)
                        } ?: emptyList())
                    }
                } catch (e: Exception) {
                    Toast.makeText(context, "Ошибка: ${e.message}", Toast.LENGTH_SHORT).show()
                }
            }
        }

        LazyColumn(modifier = Modifier.fillMaxSize()) {
            items(schedules) { schedule ->
                ScheduleRow(schedule)
            }
        }
    }
}


@RequiresApi(Build.VERSION_CODES.O)
fun getDayOfWeek(date: LocalDate): String {
    return when (date.dayOfWeek) {
        java.time.DayOfWeek.MONDAY -> "Понедельник"
        java.time.DayOfWeek.TUESDAY -> "Вторник"
        java.time.DayOfWeek.WEDNESDAY -> "Среда"
        java.time.DayOfWeek.THURSDAY -> "Четверг"
        java.time.DayOfWeek.FRIDAY -> "Пятница"
        java.time.DayOfWeek.SATURDAY -> "Суббота"
        java.time.DayOfWeek.SUNDAY -> "Воскресенье"
    }
}
