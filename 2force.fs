#version 460 core
in vec2 TexCoords;
uniform sampler2D uv1;
layout(location=1) out vec3 uv0;
void main()//dt=0.8 advect.fs force.fs advectcolor 的时间需要一致 main->mian error 3001
{
	float dt=0.05;
	float force_radius=500.0;
	float f_strength=0.1;
	//float y=-(gl_FragCoord.y-300.0);
	//float x=gl_FragCoord.x-400.0;
	//float maxdix=500;
	float y=1.0;
	float x=1.0;
	float d2=(gl_FragCoord.x-600)*(gl_FragCoord.x-600)+(gl_FragCoord.y-300)*(gl_FragCoord.y-300);
	vec2 forcedir=normalize(vec2(y,x));//xuan zhuan
	//vec3 force=vec3(gl_FragCoord.y/600,0.0,0.0);
	//float dis=pow(x*x+y*y,0.5)+0.3;
	//float c=pow(dis/500.0,9);
	//float c=0.015;
	//float c=0.015;
	float factor=exp(-(d2/force_radius));
	//vec3 f=vec3(c*force.x*dt,c*force.y*dt,0.0);//f*t
	vec2 fdt=forcedir*f_strength*factor*dt;
	vec3 uv=vec3(texture(uv1,TexCoords));
	uv0=uv+vec3(fdt,0.0);//set the p0 for pressure jacob iteration
}
