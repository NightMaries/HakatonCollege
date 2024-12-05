package com.example.schedule

import retrofit2.Response
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.Path

interface GroupService {
    @GET("group/{id}")
    suspend fun getGroupById(
        @Path("id") groupId: Int,
        @Header("Authorization") token: String
    ): Response<Group> // Группа, содержащая название

    @GET("/Group")
    suspend fun getGroup(@Header("Authorization") token: String): Response<List<Group>>
}
