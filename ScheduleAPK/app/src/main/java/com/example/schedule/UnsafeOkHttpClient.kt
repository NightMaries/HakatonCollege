package com.example.schedule

import java.security.cert.X509Certificate
import javax.net.ssl.SSLContext
import javax.net.ssl.TrustManager
import javax.net.ssl.X509TrustManager

object UnsafeOkHttpClient {

    fun getUnsafeSslSocketFactory(): javax.net.ssl.SSLSocketFactory {
        try {
            val trustAllCertificates: TrustManager = object : X509TrustManager {
                override fun getAcceptedIssuers(): Array<X509Certificate>? = emptyArray()
                override fun checkClientTrusted(chain: Array<out X509Certificate>?, authType: String?) {}
                override fun checkServerTrusted(chain: Array<out X509Certificate>?, authType: String?) {}
            }

            val sslContext = SSLContext.getInstance("TLS")
            sslContext.init(null, arrayOf(trustAllCertificates), java.security.SecureRandom())

            return sslContext.socketFactory
        } catch (e: Exception) {
            throw RuntimeException("Failed to create an SSLSocketFactory", e)
        }
    }

    fun getUnsafeTrustManager(): X509TrustManager {
        return object : X509TrustManager {
            override fun getAcceptedIssuers(): Array<X509Certificate>? = emptyArray()
            override fun checkClientTrusted(chain: Array<out X509Certificate>?, authType: String?) {}
            override fun checkServerTrusted(chain: Array<out X509Certificate>?, authType: String?) {}
        }
    }
}
