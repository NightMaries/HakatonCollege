package com.example.schedule

import retrofit2.Response
import retrofit2.http.GET
import retrofit2.http.Header



interface StudentService {
    @GET("/Student")
    suspend fun getStudent(@Header("Authorization") token: String): Response<List<Student>>
    @GET("Student")
    suspend fun getStudentInfo(
        @Header("Authorization") token: String
    ): Response<Student>
}

// В ApiClient
val studentService: StudentService by lazy {
    ApiClient.retrofit.create(StudentService::class.java)
}
