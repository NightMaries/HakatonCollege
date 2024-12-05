package com.example.schedule

import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider


class ProfileViewModelFactory(private val userService: UserService, private val token: String) :
    ViewModelProvider.Factory {
    override fun <T : ViewModel> create(modelClass: Class<T>): T {
        if (modelClass.isAssignableFrom(ProfileViewModel::class.java)) {
            @Suppress("UNCHECKED_CAST")
            return ProfileViewModel(userService, token) as T
        }
        throw IllegalArgumentException("Unknown ViewModel class")
    }
}
