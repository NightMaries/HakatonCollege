package com.example.schedule

data class Student(
    val fio: String,
    val userId: Int,
    val groupId: Int
) : List<Student> {
    override val size: Int
        get() = TODO("Not yet implemented")

    override fun contains(element: Student): Boolean {
        TODO("Not yet implemented")
    }

    override fun containsAll(elements: Collection<Student>): Boolean {
        TODO("Not yet implemented")
    }

    override fun get(index: Int): Student {
        TODO("Not yet implemented")
    }

    override fun indexOf(element: Student): Int {
        TODO("Not yet implemented")
    }

    override fun isEmpty(): Boolean {
        TODO("Not yet implemented")
    }

    override fun iterator(): Iterator<Student> {
        TODO("Not yet implemented")
    }

    override fun lastIndexOf(element: Student): Int {
        TODO("Not yet implemented")
    }

    override fun listIterator(): ListIterator<Student> {
        TODO("Not yet implemented")
    }

    override fun listIterator(index: Int): ListIterator<Student> {
        TODO("Not yet implemented")
    }

    override fun subList(fromIndex: Int, toIndex: Int): List<Student> {
        TODO("Not yet implemented")
    }
}

typealias Students = List<Student>