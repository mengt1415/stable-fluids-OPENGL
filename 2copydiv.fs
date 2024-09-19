#version 460 core
in vec2 TexCoords;
uniform sampler2D uv3; //tex0
layout(location=2) out vec3 uv2;//tex2
//this shader is to maintain the boundary condition of pressure
// after one jacob iteration,the p1 is store in the uv1.z of uvfb(GL_TEXTURE0)
//we process the boundary condition and store it back to the uv0.z(GL_TEXTURE1)
void main()
{

	
	uv2=vec3(texture(uv3,TexCoords));//-vec3(1.5,0.0,0.0);
	
}