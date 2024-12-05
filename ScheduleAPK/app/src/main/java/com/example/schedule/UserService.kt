package com.example.schedule

import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.POST
import retrofit2.http.Query
import retrofit2.Response

interface UserService {

    @GET("User/Login")
    suspend fun loginUser(
        @Query("Login") login: String,
        @Query("Password") password: String
    ): Response<JsonResponse>

    @GET("User")
    suspend fun getUsers(): List<User>

    @POST("User")
    suspend fun createUser(@Body request: CreateUserRequest): User




}