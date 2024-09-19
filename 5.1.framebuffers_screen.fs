#version 460 core
out vec4 FragColor;
in vec2 TexCoords;
uniform sampler2D screenTexture;

void main()
{
	vec3 col=texture(screenTexture,TexCoords).rgb;
	//hdr 1
	//float gamma=2.2;
	//vec3 mapped=col/(col+vec3(4.0));
	//mapped=pow(mapped,vec3(1.0/gamma));
	//FragColor=vec4(mapped,1.0);

	//hdr2
	float exposure=0.7;
	float gamma=2.2;
	vec3 mapped=vec3(1.0)-exp(-col*exposure);
	mapped=pow(mapped,vec3(1.0/gamma));
	FragColor=vec4(mapped,1.0);


	//FragColor=(averge,averge,averge,1.0);//error:0(10) : error C7011: implicit cast from "float" to "vec4"
	//FragColor=vec4(0.0,0.0,col.z,1.0);//test pressure
	//FragColor=vec4(col,1.0);//the correct one
	//FragColor=vec4(col.x,0.0,0.0,1.0);//test divergence 
	//FragColor=vec4(0.0,0.0,0.1*col.z+0.5,1.0);//test pressure
	//FragColor=vec4(col.x,col.y,0.0,1.0);//test gradient test velocity
}