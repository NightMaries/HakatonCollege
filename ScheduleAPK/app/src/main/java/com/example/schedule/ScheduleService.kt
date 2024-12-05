package com.example.schedule


import com.example.schedule.ApiClient.retrofit
import retrofit2.Response
import retrofit2.http.GET
import retrofit2.http.Header

interface ScheduleService {
    @GET("Schedule")
    suspend fun getSchedules(
        @Header("Authorization") token: String
    ): Response<List<Schedule>>

}

val scheduleService: ScheduleService by lazy {
    retrofit.create(ScheduleService::class.java)
}