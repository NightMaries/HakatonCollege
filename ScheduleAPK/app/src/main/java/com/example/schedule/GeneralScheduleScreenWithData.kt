// GeneralScheduleScreenWithData.kt
package com.example.schedule
import androidx.compose.runtime.*


@Composable
fun GeneralScheduleScreenWithData(token: String) {
    val allSchedules = remember { mutableStateOf<List<Schedule>>(emptyList()) }
    val filteredSchedules = remember { mutableStateOf<List<Schedule>>(emptyList()) }

    LaunchedEffect(true) {
        val scheduleData = getSchedule(token = token)
        allSchedules.value = scheduleData
        filteredSchedules.value = scheduleData
    }

    var searchText by remember { mutableStateOf("") }

    val onSearch: (String) -> Unit = { query ->
        filteredSchedules.value = allSchedules.value.filter { schedule ->
            schedule.subjectName.contains(query, ignoreCase = true) ||
                    schedule.groupName.contains(query, ignoreCase = true) ||
                    schedule.teacherFIO.contains(query, ignoreCase = true)
        }
    }

    GeneralScheduleScreen(
        scheduleData = filteredSchedules.value,
        searchText = searchText,
        onSearch = onSearch,
        onSearchTextChange = { searchText = it }
    )
}


suspend fun getSchedule(token: String): List<Schedule> {
    try {
        val response = scheduleService.getSchedules("Bearer $token")
        if (response.isSuccessful) {
            return response.body() ?: emptyList()
        } else {
            throw Exception("Ошибка загрузки расписания: ${response.code()}")
        }
    } catch (e: Exception) {
        throw Exception("Ошибка при запросе данных: ${e.message}")
    }
}
