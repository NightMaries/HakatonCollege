package com.example.schedule

data class UserInfo(
    val id: Int,
    val login: String,
    val token: String,
    val groupId: Int,
    val roleId: Int,
    val role: String?,
    val fio: String?,
    val subscription: Boolean
)
