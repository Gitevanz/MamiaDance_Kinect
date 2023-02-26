if not exist Assets\AzureKinect\Plugins mkdir Assets\AzureKinect\Plugins

set PACKAGE_BTSDK=packages\Microsoft.Azure.Kinect.BodyTracking.1.1.2
set PACKAGE_ONNXRUNTIME=packages\Microsoft.Azure.Kinect.BodyTracking.ONNXRuntime.1.10.0
set PACKAGE_SENSOR_SDK=packages\Microsoft.Azure.Kinect.Sensor.1.4.1
set PACKAGE_SYSTEM_BUFFERS=packages\System.Buffers.4.4.0
set PACKAGE_SYSTEM_MEMORY=packages\System.Memory.4.5.3
set PACKAGE_SYSTEM_RUNTIME_SERVICES=packages\System.Runtime.CompilerServices.Unsafe.4.5.2
set PACKAGE_SYSTEM_REFLECTION=packages\System.Reflection.Emit.Lightweight.4.6.0

copy %PACKAGE_SENSOR_SDK%\lib\netstandard2.0\Microsoft.Azure.Kinect.Sensor.dll Assets\AzureKinect\Plugins
copy %PACKAGE_SENSOR_SDK%\lib\netstandard2.0\Microsoft.Azure.Kinect.Sensor.pdb Assets\AzureKinect\Plugins
copy %PACKAGE_SENSOR_SDK%\lib\netstandard2.0\Microsoft.Azure.Kinect.Sensor.deps.json Assets\AzureKinect\Plugins
copy %PACKAGE_SENSOR_SDK%\lib\netstandard2.0\Microsoft.Azure.Kinect.Sensor.xml Assets\AzureKinect\Plugins
copy %PACKAGE_SENSOR_SDK%\lib\native\amd64\release\depthengine_2_0.dll Assets\AzureKinect\Plugins
copy %PACKAGE_SENSOR_SDK%\lib\native\amd64\release\k4a.dll Assets\AzureKinect\Plugins
copy %PACKAGE_SENSOR_SDK%\lib\native\amd64\release\k4arecord.dll Assets\AzureKinect\Plugins
copy %PACKAGE_SYSTEM_BUFFERS%\lib\netstandard2.0\System.Buffers.dll Assets\AzureKinect\Plugins
copy %PACKAGE_SYSTEM_MEMORY%\lib\netstandard2.0\System.Memory.dll Assets\AzureKinect\Plugins
copy %PACKAGE_SYSTEM_RUNTIME_SERVICES%\lib\netstandard2.0\System.Runtime.CompilerServices.Unsafe.dll Assets\AzureKinect\Plugins
copy %PACKAGE_SYSTEM_REFLECTION%\lib\netstandard2.0\System.Reflection.Emit.Lightweight.dll Assets\AzureKinect\Plugins

copy %PACKAGE_BTSDK%\lib\netstandard2.0\Microsoft.Azure.Kinect.BodyTracking.dll Assets\AzureKinect\Plugins
copy %PACKAGE_BTSDK%\lib\netstandard2.0\Microsoft.Azure.Kinect.BodyTracking.pdb Assets\AzureKinect\Plugins
copy %PACKAGE_BTSDK%\lib\netstandard2.0\Microsoft.Azure.Kinect.BodyTracking.deps.json Assets\AzureKinect\Plugins
copy %PACKAGE_BTSDK%\lib\netstandard2.0\Microsoft.Azure.Kinect.BodyTracking.xml Assets\AzureKinect\Plugins
copy %PACKAGE_BTSDK%\lib\native\amd64\release\k4abt.dll Assets\AzureKinect\Plugins
copy %PACKAGE_ONNXRUNTIME%\lib\native\amd64\release\directml.dll Assets\AzureKinect\Plugins
copy %PACKAGE_ONNXRUNTIME%\lib\native\amd64\release\onnxruntime.dll Assets\AzureKinect\Plugins
copy %PACKAGE_ONNXRUNTIME%\lib\native\amd64\release\onnxruntime_providers_cuda.dll Assets\AzureKinect\Plugins
copy %PACKAGE_ONNXRUNTIME%\lib\native\amd64\release\onnxruntime_providers_shared.dll Assets\AzureKinect\Plugins
copy %PACKAGE_ONNXRUNTIME%\lib\native\amd64\release\onnxruntime_providers_tensorrt.dll Assets\AzureKinect\Plugins

copy %PACKAGE_BTSDK%\content\dnn_model_2_0_op11.onnx .\
copy %PACKAGE_ONNXRUNTIME%\lib\native\amd64\release\directml.dll .\
copy %PACKAGE_ONNXRUNTIME%\lib\native\amd64\release\onnxruntime.dll .\
copy %PACKAGE_ONNXRUNTIME%\lib\native\amd64\release\onnxruntime_providers_cuda.dll .\
copy %PACKAGE_ONNXRUNTIME%\lib\native\amd64\release\onnxruntime_providers_shared.dll .\
copy %PACKAGE_ONNXRUNTIME%\lib\native\amd64\release\onnxruntime_providers_tensorrt.dll .\
