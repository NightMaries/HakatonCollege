package com.example.schedule

import com.google.gson.GsonBuilder
import okhttp3.OkHttpClient
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.util.concurrent.TimeUnit

object ApiClient {
    private const val BASE_URL = "https://4824-212-96-98-242.ngrok-free.app/"

    private val client = OkHttpClient.Builder()
        .connectTimeout(30, TimeUnit.SECONDS)
        .readTimeout(30, TimeUnit.SECONDS)
        .writeTimeout(30, TimeUnit.SECONDS)
        .sslSocketFactory(UnsafeOkHttpClient.getUnsafeSslSocketFactory(), UnsafeOkHttpClient.getUnsafeTrustManager())
        .hostnameVerifier { _, _ -> true }
        .build()

    private val gson = GsonBuilder()
        .setLenient()
        .create()

    val retrofit: Retrofit by lazy {
        Retrofit.Builder()
            .baseUrl(BASE_URL)
            .client(client)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
    }

    val userService: UserService by lazy {
        retrofit.create(UserService::class.java)
    }
    val scheduleService: ScheduleService by lazy {
        retrofit.create(ScheduleService::class.java)
    }
    val groupService: GroupService by lazy {
        retrofit.create(GroupService::class.java)
    }

}
