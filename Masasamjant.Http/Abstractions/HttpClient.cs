﻿using Masasamjant.Http.Interceptors;
using Masasamjant.Http.Listeners;

namespace Masasamjant.Http.Abstractions
{
    /// <summary>
    /// Represents abstract HTTP client.
    /// </summary>
    public abstract class HttpClient : IHttpClient
    {
        /// <summary>
        /// Gets the <see cref="HttpGetRequestInterceptorCollection"/> for interceptors executed 
        /// when HTTP GET request is performed.
        /// </summary>
        public HttpGetRequestInterceptorCollection HttpGetRequestInterceptors { get; } = new HttpGetRequestInterceptorCollection();

        /// <summary>
        /// Gets the <see cref="HttpPostRequestInterceptorCollection"/> for interceptors executed 
        /// when HTTP POST request is performed.
        /// </summary>
        public HttpPostRequestInterceptorCollection HttpPostRequestInterceptors { get; } = new HttpPostRequestInterceptorCollection();

        /// <summary>
        /// Gets the <see cref="HttpClientListenerCollection"/> for listeners of HTTP request execution.
        /// </summary>
        public HttpClientListenerCollection HttpClientListeners { get; } = new HttpClientListenerCollection();

        /// <summary>
        /// Perform HTTP GET request using specified <see cref="HttpGetRequest"/>.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="request">The <see cref="HttpGetRequest"/> to perform.</param>
        /// <returns>A <typeparamref name="T"/> result of request or default.</returns>
        /// <exception cref="HttpRequestException">If exception occurs when executing request.</exception>
        public abstract Task<T?> GetAsync<T>(HttpGetRequest request);

        /// <summary>
        /// Perform HTTP POST request using specified <see cref="HttpPostRequest{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the posted data and result.</typeparam>
        /// <param name="request">The <see cref="HttpPostRequest{T}"/> to perform.</param>
        /// <returns>A <typeparamref name="T"/> result of request or default.</returns>
        /// <exception cref="HttpRequestException">If exception occurs when executing request.</exception>
        public abstract Task<T?> PostAsync<T>(HttpPostRequest<T> request) where T : notnull;

        /// <summary>
        /// Perform HTTP POST request using specified <see cref="HttpPostRequest{T}"/>.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="T">The type of the posted data.</typeparam>
        /// <param name="request">The <see cref="HttpPostRequest{T}"/> to perform.</param>
        /// <returns>A <typeparamref name="TResult"/> result of request or default.</returns>
        /// <exception cref="HttpRequestException">If exception occurs when executing request.</exception>
        public abstract Task<TResult?> PostAsync<TResult, T>(HttpPostRequest<T> request) where T : notnull;

        /// <summary>
        /// Perform HTTP POST request using specified <see cref="HttpPostRequest"/>.
        /// </summary>
        /// <param name="request">The <see cref="HttpPostRequest"/> to perform.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="HttpRequestException">If exception occurs when executing request.</exception>
        public abstract Task PostAsync(HttpPostRequest request);

        /// <summary>
        /// Executed interceptors to specified <see cref="HttpGetRequest"/> and checks after each interceptor, 
        /// if request should be canceled. If one of the interceptors indicate that request should be canceled, 
        /// then remaining interceptors are not executed.
        /// </summary>
        /// <param name="request">The intercepted <see cref="HttpGetRequest"/>.</param>
        /// <returns><c>true</c> if <paramref name="request"/> should be canceled; <c>false</c> otherwise.</returns>
        protected async Task<bool> CancelAfterInterceptorsAsync(HttpGetRequest request)
        {
            // Execute each interceptor and check if some indicated that request should be canceled.
            foreach (var interceptor in HttpGetRequestInterceptors)
            { 
                var interception = await interceptor.InterceptAsync(request);

                if (interception == HttpRequestInterception.Cancel)
                    return true;
            }

            // No interceptors or none indicated cancellation.
            return false;
        }

        /// <summary>
        /// Executed interceptors to specified <see cref="HttpPostRequest"/> and checks after each interceptor, 
        /// if request should be canceled. If one of the interceptors indicate that request should be canceled, 
        /// then remaining interceptors are not executed.
        /// </summary>
        /// <param name="request">The intercepted <see cref="HttpPostRequest"/>.</param>
        /// <returns><c>true</c> if <paramref name="request"/> should be canceled; <c>false</c> otherwise.</returns>
        protected async Task<bool> CancelAfterInterceptorsAsync(HttpPostRequest request)
        {
            // Execute each interceptor and check if some indicated that request should be canceled.
            foreach (var interceptor in HttpPostRequestInterceptors)
            {
                var interception = await interceptor.InterceptAsync(request);

                if (interception == HttpRequestInterception.Cancel)
                    return true;
            }

            // No interceptors or none indicated cancellation.
            return false;
        }

        /// <summary>
        /// Invokes <see cref="IHttpClientListener.OnExecutingAsync(HttpRequest)"/> of each listener in <see cref="HttpClientListeners"/> 
        /// with specified <see cref="HttpRequest"/>.
        /// </summary>
        /// <param name="request">The <see cref="HttpRequest"/> about to be executed.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        protected async Task OnExecutingHttpClientListenersAsync(HttpRequest request)
        {
            foreach (var listener in HttpClientListeners)
                await listener.OnExecutingAsync(request);
        }

        /// <summary>
        /// Invokes <see cref="IHttpClientListener.OnExecutedAsync(HttpRequest)"/> of each listener in <see cref="HttpClientListeners"/> 
        /// with specified <see cref="HttpRequest"/>.
        /// </summary>
        /// <param name="request">The executed <see cref="HttpRequest"/>.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        protected async Task OnExecutedHttpClientListenersAsync(HttpRequest request)
        {
            foreach (var listener in HttpClientListeners)
                await listener.OnExecutedAsync(request);
        }

        /// <summary>
        /// Invokes <see cref="IHttpClientListener.OnErrorAsync(HttpRequest, Exception)"/> of each listener in <see cref="HttpClientListeners"/>
        /// with specified <see cref="HttpRequest"/> and <see cref="Exception"/>.
        /// </summary>
        /// <param name="request">The <see cref="HttpRequest"/> where exception occurred.</param>
        /// <param name="exception">The occurred <see cref="Exception"/>.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        protected async Task OnErrorHttpClientListenersAsync(HttpRequest request, Exception exception)
        {
            foreach (var listener in HttpClientListeners)
                await listener.OnErrorAsync(request, exception);
        }
    }
}