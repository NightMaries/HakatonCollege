package com.example.schedule

data class User(
    val id: Int,
    val login: String,
    val passwordHash: String,
    val token: String?,
    val roleId: Int,
    val role: String?
)
