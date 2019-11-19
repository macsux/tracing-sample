Shows how to get zipkin tracing on PCF



1. Clone the repo
2. [Download Zipkin](https://search.maven.org/remote_content?g=io.zipkin&a=zipkin-server&v=LATEST&c=exec) into repo root
3. Adjust manifest.yaml `management__tracing__exporter__zipkin__endpoint` env var to point to URL of zipkin apps (watch not to override the path after the domain or it won't work)
4. cf push
5. Hit sample app at `/WeatherForecast` endpoint
6. Open zipkin and observe the trace

