
import android.os.Build
import android.util.Log
import androidx.annotation.RequiresApi
import androidx.compose.runtime.*
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.schedule.ApiClient.scheduleService
import com.example.schedule.Schedule
import com.example.schedule.ScheduleService
import kotlinx.coroutines.launch
import java.time.LocalTime

@RequiresApi(Build.VERSION_CODES.O)
class ScheduleViewModel : ViewModel() {
    private val _schedules = mutableStateListOf<Schedule>()
    val schedules: List<Schedule> get() = _schedules

    private val _filteredSchedules = mutableStateListOf<Schedule>()
    val filteredSchedules: List<Schedule> get() = _filteredSchedules

    var nextClass: Schedule? by mutableStateOf(null)
        private set

    var timeUntilNextClass: Long? by mutableStateOf(null)
        private set

    fun loadSchedules(token: String, currentDay: String, currentTime: LocalTime) {
        viewModelScope.launch {
            try {
                // Выполняем запрос и ожидаем ответ типа Response<List<Schedule>>
                val response = scheduleService.getSchedules(token)
                if (response.isSuccessful) {
                    // Получаем тело ответа (список расписаний)
                    response.body()?.let { schedulesList ->
                        _schedules.clear()
                        _schedules.addAll(schedulesList)

                        val filtered = _schedules.filter { it.weekDay == currentDay }
                        _filteredSchedules.clear()
                        _filteredSchedules.addAll(
                            if (filtered.isNotEmpty()) filtered else _schedules.filter { it.weekDay == "Понедельник" }
                        )

                        calculateNextClass(currentTime)
                    }
                } else {
                    Log.e("ScheduleViewModel", "Response not successful: ${response.code()}")
                }
            } catch (e: Exception) {
                Log.e("ScheduleViewModel", "Error loading schedules", e)  // Логируем ошибку
            }
        }
    }




    private fun calculateNextClass(currentTime: LocalTime) {
        nextClass = _filteredSchedules
            .filter { parseTime(it.scheduleStart).isAfter(currentTime) }
            .minByOrNull { parseTime(it.scheduleStart) }

        timeUntilNextClass = nextClass?.let {
            java.time.Duration.between(currentTime, parseTime(it.scheduleStart)).toMinutes()
        }
    }

    companion object {
        @RequiresApi(Build.VERSION_CODES.O)
        fun parseTime(timeString: String): LocalTime {
            return LocalTime.parse(timeString)
        }
    }
}
