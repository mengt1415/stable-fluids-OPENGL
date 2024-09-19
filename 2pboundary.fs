#version 460 core
in vec2 TexCoords;
uniform sampler2D uv1;
layout(location=1) out vec3 boundarycolor;
//this shader is to maintain the boundary condition of pressure
// after one jacob iteration,the p1 is store in the uv1.z of uvfb(GL_TEXTURE0)
//we process the boundary condition and store it back to the uv0.z(GL_TEXTURE1)
void main()
{

	
	boundarycolor=vec3(texture(uv1,TexCoords));
	
}