package com.example.schedule.api

import com.example.schedule.Group
import com.example.schedule.Student
import com.example.schedule.Teacher
import com.example.schedule.User
import retrofit2.Call
import retrofit2.Response
import retrofit2.http.GET
import retrofit2.http.Header

interface ApiService {
    @GET("User")
    fun getUsers(): Call<List<User>>

    @GET("/Student")
    suspend fun getStudent(@Header("Authorization") token: String): Response<List<Student>>



    @GET("/Teacher")
    suspend fun getTeacher(@Header("Authorization") token: String): Teacher


}
