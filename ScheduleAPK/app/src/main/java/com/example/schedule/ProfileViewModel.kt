package com.example.schedule

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.schedule.ApiClient.groupService
import com.example.schedule.api.ApiService
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.launch
import retrofit2.Response

class ProfileViewModel(
    private val userService: UserService,
    private val token: String
) : ViewModel() {
    private val _studentInfo = MutableStateFlow<Student?>(null)
    val studentInfo: StateFlow<Student?> = _studentInfo

    private val _groupInfo = MutableStateFlow<Group?>(null)
    val groupInfo: StateFlow<Group?> = _groupInfo

    init {
        fetchData()
    }

    private fun fetchData() {
        viewModelScope.launch {
            try {
                // Получение данных студента
                val studentResponse: Response<List<Student>> = studentService.getStudent("Bearer $token")
                if (studentResponse.isSuccessful) {
                    // Берем первый элемент списка, если он есть
                    _studentInfo.value = studentResponse.body()?.firstOrNull()
                }

                // Получение данных группы
                val groupResponse: Response<List<Group>> = groupService.getGroup("Bearer $token")
                if (groupResponse.isSuccessful) {
                    // Берем первый элемент списка, если он есть
                    _groupInfo.value = groupResponse.body()?.firstOrNull()
                }
            } catch (e: Exception) {
                // Обработка ошибок (например, логирование)
            }
        }
    }
}
