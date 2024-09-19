#include <glad/glad.h>
#include <GLFW/glfw3.h>
#include "stb_image.h"
#define STB_IMAGE_IMPLEMENTATION
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>
//#include<Windows.h>
#include <assimp/Importer.hpp>
#include <assimp/scene.h>
#include <assimp/postprocess.h>
#include <opencv2/highgui.hpp>
#include <opencv2/imgproc.hpp>
#include<vector>
#include<map>

#include "shader_m.h"
#include "camera.h"
#include "model.h"

#include <iostream>
void framebuffer_size_callback(GLFWwindow* window, int width, int height);
void mouse_callback(GLFWwindow* window, double xpos, double ypos);
void scroll_callback(GLFWwindow* window, double xoffset, double yoffset);
void processInput(GLFWwindow* window);
unsigned int loadTexture(const char* path);



//在framebuffer.vs 的 in vec2 aTexCoords 中，location误写 =0 正确的应该是=1
//然后渲染出错



// settings
const unsigned int SCR_WIDTH = 800;
const unsigned int SCR_HEIGHT = 800;
const float gridsize = 800;
//const unsigned int SCR_HEIGHT = 600;
// 800m 800m
//dx=1m
//rdx=1m  1/dx

// camera
Camera camera(glm::vec3(0.0f, 0.0f, 3.0f));
float lastX = (float)SCR_WIDTH / 2.0;
float lastY = (float)SCR_HEIGHT / 2.0;
bool firstMouse = true;

// timing
float deltaTime = 0.0f;
float lastFrame = 0.0f;

int main()
{
    glfwInit();
    glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
    glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
    glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

#ifdef __APPLE__
    glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GL_TRUE);
#endif

    // glfw window creation
    // --------------------
    GLFWwindow* window = glfwCreateWindow(SCR_WIDTH, SCR_HEIGHT, "LEARN OPENGL", NULL, NULL);
    if (window == NULL)
    {
        std::cout << "Failed to create GLFW window" << std::endl;
        glfwTerminate();
        return -1;
    }
    glfwMakeContextCurrent(window);
    glfwSetFramebufferSizeCallback(window, framebuffer_size_callback);
    glfwSetCursorPosCallback(window, mouse_callback);
    glfwSetScrollCallback(window, scroll_callback);

    // tell GLFW to capture our mouse
    glfwSetInputMode(window, GLFW_CURSOR, GLFW_CURSOR_DISABLED);

    // glad: load all OpenGL function pointers
    // ---------------------------------------
    if (!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress))
    {
        std::cout << "Failed to initialize GLAD" << std::endl;
        return -1;
    }

    // configure global opengl state
    // -----------------------------
    glEnable(GL_DEPTH_TEST);

    // build and compile shaders
    Shader shader("5.1.framebuffers.vs", "5.1.framebuffers.fs");
    Shader screenShader("5.1.framebuffers_screen.vs", "5.1.framebuffers_screen.fs");
    //Shader originalDyeshader("5.1.framebuffers_screen.vs", "2gendye.fs");
    //Shader gendye("5.1.framebuffers_screen.vs", "3gendye.fs");
    //Shader gendye("5.1.framebuffers_screen.vs", "3gendye.fs");
    //Shader advect("5.1.framebuffers_screen.vs", "2advect.fs");
    //Shader advectcolor("5.1.framebuffers_screen.vs", "2advectcolor.fs");
    //Shader force("5.1.framebuffers_screen.vs", "2force.fs");

    Shader vboundary("5.1.framebuffers_screen.vs", "2vboundary.fs");
    //Shader cboundary("5.1.framebuffers_screen.vs", "2cboundary.fs");
    //Shader div("5.1.framebuffers_screen.vs", "2div.fs");
    //Shader jacob("5.1.framebuffers_screen.vs", "2jacob.fs");
   // Shader pboundary("5.1.framebuffers_screen.vs", "2pboundary.fs");
    //Shader gradient("5.1.framebuffers_screen.vs", "2gradient.fs");
    Shader vboundary2("5.1.framebuffers_screen.vs", "2vboundary2.fs");
    //Shader vort("5.1.framebuffers_screen.vs", "2vort.fs");
    Shader copydiv("5.1.framebuffers_screen.vs", "2copydiv.fs");
    //Shader gradient2("5.1.framebuffers_screen.vs", "2testgradient.fs");

    Shader gendye("5.1.framebuffers_screen.vs", "3gendye.fs");
    Shader genv("5.1.framebuffers_screen.vs", "3genv.fs");
    Shader copyv("5.1.framebuffers_screen.vs", "3copyv.fs");
    Shader advect("5.1.framebuffers_screen.vs", "3advect.fs");
   // Shader force("5.1.framebuffers_screen.vs", "3force.fs");
    Shader div("5.1.framebuffers_screen.vs", "3div.fs");
    Shader jacob("5.1.framebuffers_screen.vs", "3jacob.fs");
    Shader jacob2("5.1.framebuffers_screen.vs", "3jacob2.fs");
    Shader pboundary("5.1.framebuffers_screen.vs", "3pboundary.fs");
    Shader gradient2("5.1.framebuffers_screen.vs", "32testgradient.fs");
    Shader gradient("5.1.framebuffers_screen.vs", "3gradient.fs");
    Shader advectcolor("5.1.framebuffers_screen.vs", "3advectcolor.fs");
    Shader cboundary("5.1.framebuffers_screen.vs", "3cboundary.fs");
    Shader vort("5.1.framebuffers_screen.vs", "3vort.fs");
    //Shader adddye("5.1.framebuffers_screen.vs", "3forcedye.fs");//acting like force
    /// <shaders in sf3>
    copyv.use();
    copyv.setInt("uv1", 0);
    /// <the end of shaders in sf3>
    shader.use();
    shader.setInt("texture1", 0);

    screenShader.use();
    //screenShader.setInt("texture1", 0);
    screenShader.setInt("screenTexture", 0);
    /////////////////////// old shaders from failed project
    advect.use();
    advect.setInt("olduv", 1);//olduv is uv0 of dyefb, color_attachment1
    advect.setFloat("gridsize", gridsize);

    advectcolor.use();
    advectcolor.setInt("oldcolor", 0);//dyetexcolor
    advectcolor.setInt("uv0", 1);//uv0 //
    advectcolor.setFloat("gridsize", gridsize);
    //write to uvfb'uv5 COLOR_ATTACHMENT 2
    //scene 1
    Shader force("5.1.framebuffers_screen.vs", "3force.fs");
    force.use();
    force.setInt("uv1", 1);
    Shader adddye("5.1.framebuffers_screen.vs", "3forcedye.fs");//acting like force
    adddye.use();
    adddye.setInt("uv1", 1);

    vboundary.use();
    vboundary.setInt("uv1", 0);

    cboundary.use();
    cboundary.setInt("uv5", 1);

    div.use();
    div.setInt("uv0", 0);
    div.setFloat("gridsize", gridsize);

    jacob.use();
    jacob.setInt("p0", 2);//uv0 dyefb
    jacob.setInt("div", 1);//uv3 uvfb 
    jacob.setFloat("gridsize", gridsize);

    jacob2.use();
    jacob2.setInt("p0", 2);//uv0 dyefb
    jacob2.setInt("div", 1);//uv3 uvfb 
    jacob2.setFloat("gridsize", gridsize);

    pboundary.use();
    pboundary.setInt("uv1", 0);

    gradient.use();//this shader works just like advect,take input from uv1 and store the output in uv0
    gradient.setInt("uv0", 1);
    gradient.setFloat("gridsize", gridsize);
    // gradient.setInt("uv0", 1);

    gradient2.use();
    gradient2.setInt("uv0", 1);

    vboundary.use();
    vboundary.setInt("uv1", 0);

    vort.use();
    vort.setInt("uv0", 2);
    vort.setInt("div", 1);
    vort.setFloat("gridsize", gridsize);

    copydiv.use();
    copydiv.setInt("uv3", 0);

    float cubeVertices[] = {
        // positions          // texture Coords
        -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
         0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
         0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
         0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
        -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

        -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
         0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
         0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
         0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
        -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

        -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
        -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
        -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

         0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
         0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
         0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
         0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
         0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
         0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

        -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
         0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
         0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
         0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

        -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
         0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
         0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
         0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
        -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
        -0.5f,  0.5f, -0.5f,  0.0f, 1.0f
    };
    float planeVertices[] = {
        // positions          // texture Coords 
         5.0f, -0.5f,  5.0f,  2.0f, 0.0f,
        -5.0f, -0.5f,  5.0f,  0.0f, 0.0f,
        -5.0f, -0.5f, -5.0f,  0.0f, 2.0f,

         5.0f, -0.5f,  5.0f,  2.0f, 0.0f,
        -5.0f, -0.5f, -5.0f,  0.0f, 2.0f,
         5.0f, -0.5f, -5.0f,  2.0f, 2.0f
    };
    float quadVertices[] = { // vertex attributes for a quad that fills the entire screen in Normalized Device Coordinates.
        // positions   // texCoords
        -1.0f,  1.0f,  0.0f, 1.0f,
        -1.0f, -1.0f,  0.0f, 0.0f,
         1.0f, -1.0f,  1.0f, 0.0f,

        -1.0f,  1.0f,  0.0f, 1.0f,
         1.0f, -1.0f,  1.0f, 0.0f,
         1.0f,  1.0f,  1.0f, 1.0f
    };
    //cube VAO VBO
    unsigned int cubeVAO, cubeVBO;
    {glGenVertexArrays(1, &cubeVAO);
    glBindVertexArray(cubeVAO);
    glGenBuffers(1, &cubeVBO);
    glBindBuffer(GL_ARRAY_BUFFER, cubeVBO);
    glBufferData(GL_ARRAY_BUFFER, sizeof(cubeVertices), &cubeVertices, GL_STATIC_DRAW);
    glEnableVertexAttribArray(0);
    glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 5 * sizeof(float), (void*)0);
    glEnableVertexAttribArray(1);
    glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, 5 * sizeof(float), (void*)(3 * sizeof(float)));
    glBindVertexArray(0); }

    //plane VAO VBO
    {unsigned int planeVAO, planeVBO;
    glGenVertexArrays(1, &planeVAO);
    glGenBuffers(1, &planeVBO);
    glBindVertexArray(planeVAO);
    glBindBuffer(GL_ARRAY_BUFFER, planeVBO);
    glBufferData(GL_ARRAY_BUFFER, sizeof(planeVertices), &planeVertices, GL_STATIC_DRAW);
    glEnableVertexAttribArray(0);
    glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 5 * sizeof(float), (void*)0);
    glEnableVertexAttribArray(1);
    glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, 5 * sizeof(float), (void*)(3 * sizeof(float))); }
    // screen quad VAO
    unsigned int quadVAO, quadVBO;
    {glGenVertexArrays(1, &quadVAO);
    glGenBuffers(1, &quadVBO);
    glBindVertexArray(quadVAO);
    glBindBuffer(GL_ARRAY_BUFFER, quadVBO);
    glBufferData(GL_ARRAY_BUFFER, sizeof(quadVertices), &quadVertices, GL_STATIC_DRAW);
    glEnableVertexAttribArray(0);
    glVertexAttribPointer(0, 2, GL_FLOAT, GL_FALSE, 4 * sizeof(float), (void*)0);
    glEnableVertexAttribArray(1);
    glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, 4 * sizeof(float), (void*)(2 * sizeof(float))); }

    unsigned int cubeTexture = loadTexture("container.jpg");
    unsigned int floorTexture = loadTexture("metal.png");



    unsigned int framebuffer;
    {glGenFramebuffers(1, &framebuffer);
    glBindFramebuffer(GL_FRAMEBUFFER, framebuffer); }

    unsigned int textureColorbuffer;
    {glGenTextures(1, &textureColorbuffer);
    glBindTexture(GL_TEXTURE_2D, textureColorbuffer);
    //glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, SCR_WIDTH, SCR_HEIGHT, 0, GL_RGB, GL_UNSIGNED_BYTE, NULL);
    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB16F, SCR_WIDTH, SCR_HEIGHT, 0, GL_RGB, GL_FLOAT, NULL);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE); }
    //glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_BORDER);
    //glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_BORDER);
   // float borderColor[] = { 50.0f,0.f,0.0f,1.0f };
   // glTexParameterfv(GL_TEXTURE_2D, GL_TEXTURE_BORDER_COLOR, borderColor);
    glFramebufferTexture2D(GL_FRAMEBUFFER, GL_COLOR_ATTACHMENT0, GL_TEXTURE_2D, textureColorbuffer, 0);

    unsigned int rbo;
    { glGenRenderbuffers(1, &rbo);
    glBindRenderbuffer(GL_RENDERBUFFER, rbo);
    glRenderbufferStorage(GL_RENDERBUFFER, GL_DEPTH24_STENCIL8, SCR_WIDTH, SCR_HEIGHT);
    glFramebufferRenderbuffer(GL_FRAMEBUFFER, GL_DEPTH_STENCIL_ATTACHMENT, GL_RENDERBUFFER, rbo);
    if (glCheckFramebufferStatus(GL_FRAMEBUFFER) != GL_FRAMEBUFFER_COMPLETE)
        cout << "ERROR:FRAMEBUFFER IS NOT COMPLETE\n";
    glBindFramebuffer(GL_FRAMEBUFFER, 0); }

 
    // the original dye field
    /////the dye1 fb ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    unsigned int dyefb1;//     color buffer
    glGenFramebuffers(1, &dyefb1);
    glBindFramebuffer(GL_FRAMEBUFFER, dyefb1);

    unsigned int dye1;
    glGenTextures(1, &dye1);
    glBindTexture(GL_TEXTURE_2D, dye1);
    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB16F, SCR_WIDTH, SCR_HEIGHT, 0, GL_RGB, GL_FLOAT, NULL);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
    //glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_BORDER);
    //glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_BORDER);
    //float dyeborderColor[] = { 1.f,1.f,1.0f,1.0f };
    //glTexParameterfv(GL_TEXTURE_2D, GL_TEXTURE_BORDER_COLOR, dyeborderColor);
    glFramebufferTexture2D(GL_FRAMEBUFFER, GL_COLOR_ATTACHMENT0, GL_TEXTURE_2D, dye1, 0);

    unsigned int rbo2;
    glGenRenderbuffers(1, &rbo2);
    glBindRenderbuffer(GL_RENDERBUFFER, rbo2);
    glRenderbufferStorage(GL_RENDERBUFFER, GL_DEPTH24_STENCIL8, SCR_WIDTH, SCR_HEIGHT);
    glFramebufferRenderbuffer(GL_FRAMEBUFFER, GL_DEPTH_STENCIL_ATTACHMENT, GL_RENDERBUFFER, rbo2);
    if (glCheckFramebufferStatus(GL_FRAMEBUFFER) != GL_FRAMEBUFFER_COMPLETE)
        cout << "ERROR:FRAMEBUFFER IS NOT COMPLETE\n";
    glBindFramebuffer(GL_FRAMEBUFFER, 0);
    //////// the end of dye1 fb/////////////////////////////////////////////////////////////////////////////////////////////////
    

      /////the dye2 fb/////////////////////////////////////////////////////////////////////////////////////////////////////////////
    unsigned int dyefb2;//    another color buffer
    glGenFramebuffers(1, &dyefb2);
    glBindFramebuffer(GL_FRAMEBUFFER, dyefb2);

    unsigned int dye2;
    glGenTextures(1, &dye2);
    glBindTexture(GL_TEXTURE_2D, dye2);
    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB16F, SCR_WIDTH, SCR_HEIGHT, 0, GL_RGB, GL_FLOAT, NULL);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
    //glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_BORDER);
    //glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_BORDER);
    //float dyeborderColor[] = { 1.f,1.f,1.0f,1.0f };
    //glTexParameterfv(GL_TEXTURE_2D, GL_TEXTURE_BORDER_COLOR, dyeborderColor);
    glFramebufferTexture2D(GL_FRAMEBUFFER, GL_COLOR_ATTACHMENT0, GL_TEXTURE_2D, dye2, 0);

    unsigned int rbo4;
    glGenRenderbuffers(1, &rbo4);
    glBindRenderbuffer(GL_RENDERBUFFER, rbo4);
    glRenderbufferStorage(GL_RENDERBUFFER, GL_DEPTH24_STENCIL8, SCR_WIDTH, SCR_HEIGHT);
    glFramebufferRenderbuffer(GL_FRAMEBUFFER, GL_DEPTH_STENCIL_ATTACHMENT, GL_RENDERBUFFER, rbo4);
    if (glCheckFramebufferStatus(GL_FRAMEBUFFER) != GL_FRAMEBUFFER_COMPLETE)
        cout << "ERROR:FRAMEBUFFER IS NOT COMPLETE\n";
    glBindFramebuffer(GL_FRAMEBUFFER, 0);
    //////// the end of dye2 fb/////////////////////////////////////////////////////////////////////////////////////////////////

    //the original velocity field  uv0////////////////////////////////////////////////////////////////////////////////////////
    unsigned int uvfb0;// 
    glGenFramebuffers(1, &uvfb0);    //uv buffer  uv0->advect->uv3
    glBindFramebuffer(GL_FRAMEBUFFER, uvfb0);

    unsigned int uv0;
    glGenTextures(1, &uv0);
    glBindTexture(GL_TEXTURE_2D, uv0);
    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB16F, SCR_WIDTH, SCR_HEIGHT, 0, GL_RGB, GL_FLOAT, NULL);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
    // glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_BORDER);
     //glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_BORDER);
    //float uvborderColor[] = { 0.f,0.f,0.0f,1.0f };
    // glTexParameterfv(GL_TEXTURE_2D, GL_TEXTURE_BORDER_COLOR, uvborderColor);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
    glFramebufferTexture2D(GL_FRAMEBUFFER, GL_COLOR_ATTACHMENT0, GL_TEXTURE_2D, uv0, 0);

    unsigned int rbo3;
    glGenRenderbuffers(1, &rbo3);
    glBindRenderbuffer(GL_RENDERBUFFER, rbo3);
    glRenderbufferStorage(GL_RENDERBUFFER, GL_DEPTH24_STENCIL8, SCR_WIDTH, SCR_HEIGHT);
    glFramebufferRenderbuffer(GL_FRAMEBUFFER, GL_DEPTH_STENCIL_ATTACHMENT, GL_RENDERBUFFER, rbo3);
    if (glCheckFramebufferStatus(GL_FRAMEBUFFER) != GL_FRAMEBUFFER_COMPLETE)
        cout << "ERROR:FRAMEBUFFER IS NOT COMPLETE\n";
    glBindFramebuffer(GL_FRAMEBUFFER, 0);
    ////the end of uvfb0////////////////////////////////////////////////////////////////////////////////////////////////////


      //the another velocity field  uv1/////////////////////////////////////////////////////////////////////////////
    unsigned int uvfb1;// 
    glGenFramebuffers(1, &uvfb1);
    glBindFramebuffer(GL_FRAMEBUFFER, uvfb1);

    unsigned int uv1;
    glGenTextures(1, &uv1);
    glBindTexture(GL_TEXTURE_2D, uv1);
    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB16F, SCR_WIDTH, SCR_HEIGHT, 0, GL_RGB, GL_FLOAT, NULL);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);  //should be gl_nearest
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
    // glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_BORDER);
     //glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_BORDER);
    //float uvborderColor[] = { 0.f,0.f,0.0f,1.0f };
    // glTexParameterfv(GL_TEXTURE_2D, GL_TEXTURE_BORDER_COLOR, uvborderColor);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
    glFramebufferTexture2D(GL_FRAMEBUFFER, GL_COLOR_ATTACHMENT0, GL_TEXTURE_2D, uv1, 0);

    unsigned int rbo5;
    glGenRenderbuffers(1, &rbo5);
    glBindRenderbuffer(GL_RENDERBUFFER, rbo5);
    glRenderbufferStorage(GL_RENDERBUFFER, GL_DEPTH24_STENCIL8, SCR_WIDTH, SCR_HEIGHT);
    glFramebufferRenderbuffer(GL_FRAMEBUFFER, GL_DEPTH_STENCIL_ATTACHMENT, GL_RENDERBUFFER, rbo5);
    if (glCheckFramebufferStatus(GL_FRAMEBUFFER) != GL_FRAMEBUFFER_COMPLETE)
        cout << "ERROR:FRAMEBUFFER IS NOT COMPLETE\n";
    glBindFramebuffer(GL_FRAMEBUFFER, 0);
    ////the end of uvfb1 ////////////////////////////////////////////////////////////////////////////////////////////////////


          //the another velocity field  uv1/////////////////////////////////////////////////////////////////////////////
    unsigned int uvfb3;// 
    glGenFramebuffers(1, &uvfb3);
    glBindFramebuffer(GL_FRAMEBUFFER, uvfb3);

    unsigned int uv3;
    glGenTextures(1, &uv3);
    glBindTexture(GL_TEXTURE_2D, uv3);
    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB16F, SCR_WIDTH, SCR_HEIGHT, 0, GL_RGB, GL_FLOAT, NULL);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST); //should be gl_nearest
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
    // glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_BORDER);
     //glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_BORDER);
    //float uvborderColor[] = { 0.f,0.f,0.0f,1.0f };
    // glTexParameterfv(GL_TEXTURE_2D, GL_TEXTURE_BORDER_COLOR, uvborderColor);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
    glFramebufferTexture2D(GL_FRAMEBUFFER, GL_COLOR_ATTACHMENT0, GL_TEXTURE_2D, uv3, 0);

    unsigned int rbo7;
    glGenRenderbuffers(1, &rbo7);
    glBindRenderbuffer(GL_RENDERBUFFER, rbo7);
    glRenderbufferStorage(GL_RENDERBUFFER, GL_DEPTH24_STENCIL8, SCR_WIDTH, SCR_HEIGHT);
    glFramebufferRenderbuffer(GL_FRAMEBUFFER, GL_DEPTH_STENCIL_ATTACHMENT, GL_RENDERBUFFER, rbo7);
    if (glCheckFramebufferStatus(GL_FRAMEBUFFER) != GL_FRAMEBUFFER_COMPLETE)
        cout << "ERROR:FRAMEBUFFER IS NOT COMPLETE\n";
    glBindFramebuffer(GL_FRAMEBUFFER, 0);
    ////the end of uvfb1 ////////////////////////////////////////////////////////////////////////////////////////////////////


    //the uvfb2 store div and curl ////////////////////////////////////////////////////////////////////////////////
    unsigned int uvfb2;//       this buffer stores div and curl  GL_NEAREST
    glGenFramebuffers(1, &uvfb2);
    glBindFramebuffer(GL_FRAMEBUFFER, uvfb2);

    unsigned int uv2; // copy div curl
    glGenTextures(1, &uv2);
    glBindTexture(GL_TEXTURE_2D, uv2);
    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB16F, SCR_WIDTH, SCR_HEIGHT, 0, GL_RGB, GL_FLOAT, NULL);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
    // glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_BORDER);
     //glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_BORDER);
    // float uvborderColor[] = { 0.f,0.f,0.0f,1.0f };
    // glTexParameterfv(GL_TEXTURE_2D, GL_TEXTURE_BORDER_COLOR, uvborderColor);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
    glFramebufferTexture2D(GL_FRAMEBUFFER, GL_COLOR_ATTACHMENT0, GL_TEXTURE_2D, uv2, 0);

    unsigned int rbo6;
    glGenRenderbuffers(1, &rbo6);
    glBindRenderbuffer(GL_RENDERBUFFER, rbo6);
    glRenderbufferStorage(GL_RENDERBUFFER, GL_DEPTH24_STENCIL8, SCR_WIDTH, SCR_HEIGHT);
    glFramebufferRenderbuffer(GL_FRAMEBUFFER, GL_DEPTH_STENCIL_ATTACHMENT, GL_RENDERBUFFER, rbo6);
    if (glCheckFramebufferStatus(GL_FRAMEBUFFER) != GL_FRAMEBUFFER_COMPLETE)
        cout << "ERROR:FRAMEBUFFER IS NOT COMPLETE\n";
    glBindFramebuffer(GL_FRAMEBUFFER, 0);
    ////the end of uvfb2 ////////////////////////////////////////////////////////////////////////////////////////////////////


   




    //initialize the dye field and velocity field
    glBindFramebuffer(GL_FRAMEBUFFER, dyefb1);
    glEnable(GL_DEPTH_TEST); //
    glDepthFunc(GL_ALWAYS);

    // glClearColor(0.1f, 0.1f, 0.1f, 1.0f);
    // glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT);

     //gen original dye field
    glBindFramebuffer(GL_FRAMEBUFFER, dyefb1);
    gendye.use();
    glBindVertexArray(quadVAO);
    glDrawArrays(GL_TRIANGLES, 0, 6);
    glBindFramebuffer(GL_FRAMEBUFFER, 0);

    //gen original v field
    glBindFramebuffer(GL_FRAMEBUFFER, uvfb0);//shoule be ubfb0 in final version
    genv.use();
    glBindVertexArray(quadVAO);
    glDrawArrays(GL_TRIANGLES, 0, 6);
    glBindFramebuffer(GL_FRAMEBUFFER, 0);

    /*glBindFramebuffer(GL_FRAMEBUFFER, uvfb1);
    copyv.use();
    glActiveTexture(GL_TEXTURE0);
    glBindTexture(GL_TEXTURE_2D, uv0);
    glDrawArrays(GL_TRIANGLES, 0, 6);
    glBindFramebuffer(GL_FRAMEBUFFER, 0);*/

    /*glBindFramebuffer(GL_FRAMEBUFFER, uvfb0);
    copyv.use();
    glActiveTexture(GL_TEXTURE0);
    glBindTexture(GL_TEXTURE_2D, uv1);
    glDrawArrays(GL_TRIANGLES, 0, 6);
    glBindFramebuffer(GL_FRAMEBUFFER, 0);*/
    cout << GL_MAX_TEXTURE_SIZE;

    while (!glfwWindowShouldClose(window))
    {
        float currentFrame = static_cast<float>(glfwGetTime());
        deltaTime = currentFrame - lastFrame;
        lastFrame = currentFrame;

        // input
        // -----
        processInput(window);


        // ADVECT VELOCITY ////////////////////////////////////////////////////////
        //advect the uv use uv0 to compute the new uv3
        glBindFramebuffer(GL_FRAMEBUFFER, uvfb3);
        advect.use();
        glBindVertexArray(quadVAO);
        glActiveTexture(GL_TEXTURE1);
        glBindTexture(GL_TEXTURE_2D, uv0);
        glDrawArrays(GL_TRIANGLES, 0, 6);

        //copy
        /*glBindFramebuffer(GL_FRAMEBUFFER, uvfb0);
        copyv.use();
        glActiveTexture(GL_TEXTURE0);
        glBindTexture(GL_TEXTURE_2D, uv3);
        glDrawArrays(GL_TRIANGLES, 0, 6);
        glBindFramebuffer(GL_FRAMEBUFFER, 0);*/
        //the end of advect velocity//////////////////////////////////////////////




        //ADVECT COLOR////////////////////////////////////////////////////
        glBindFramebuffer(GL_FRAMEBUFFER, dyefb2);
        advectcolor.use();
        glActiveTexture(GL_TEXTURE0);
        glBindTexture(GL_TEXTURE_2D, dye1);
        glActiveTexture(GL_TEXTURE1);
        glBindTexture(GL_TEXTURE_2D, uv0);
        glDrawArrays(GL_TRIANGLES, 0, 6);

        /*glBindFramebuffer(GL_FRAMEBUFFER, dyefb1);
        cboundary.use();
        glActiveTexture(GL_TEXTURE1);
        glBindTexture(GL_TEXTURE_2D, dye2);
        glDrawArrays(GL_TRIANGLES, 0, 6);*/
        // the end of advect color ///////////////////////////////////////
        

        //FORCE  //////////////////////////////////////////////////////////////
        //take uv3 and add force ->uv1 apply force to uv1 then use uv1 to compute div
        glBindFramebuffer(GL_FRAMEBUFFER, uvfb1);
        force.use(); // set p0 for the begin of pressure jacob iteration
        glActiveTexture(GL_TEXTURE1);
        glBindTexture(GL_TEXTURE_2D, uv3);
        glDrawArrays(GL_TRIANGLES, 0, 6);

        //copy
        /*glBindFramebuffer(GL_FRAMEBUFFER, uvfb3);
        copyv.use();
        glActiveTexture(GL_TEXTURE0);
        glBindTexture(GL_TEXTURE_2D, uv1);
        glDrawArrays(GL_TRIANGLES, 0, 6);
        glBindFramebuffer(GL_FRAMEBUFFER, 0);*/

        //the end of FORCE ////////////////////////////////////////////////////

        //ADD DYE JUST LIKE ADDING FORCE////////////////////////////

        glBindFramebuffer(GL_FRAMEBUFFER, dyefb1);
        adddye.use();
        float r = sin(0.4*currentFrame) * 0.5 + 0.5;
        float g = sin(0.6*currentFrame) * 0.5 + 0.5;
        float b = sin(0.8*currentFrame) * 0.5 + 0.5;
        adddye.setFloat("r", r);
        adddye.setFloat("g", g);
        adddye.setFloat("b", b);
        glActiveTexture(GL_TEXTURE1);
        glBindTexture(GL_TEXTURE_2D, dye2);
        glDrawArrays(GL_TRIANGLES, 0, 6);


        ////////////////////////////////////////////////////////////






        // DIV CURL  /////////////////////////////////////////////////////////////////
        //compute curl in div.fs in div.fs
        glBindFramebuffer(GL_FRAMEBUFFER, uvfb2);
        glClearColor(1.0f, 1.0f, 0.1f, 1.0f);
        div.use();
        glActiveTexture(GL_TEXTURE0);
        glBindTexture(GL_TEXTURE_2D, uv1);
        glDrawArrays(GL_TRIANGLES, 0, 6);
        //copy div
        /*glBindFramebuffer(GL_FRAMEBUFFER, dyefb);
        //glDrawBuffer(GL_COLOR_ATTACHMENT2);
        glClearColor(1.0f, 1.0f, 0.1f, 1.0f);
        copydiv.use();
        glActiveTexture(GL_TEXTURE0);
        glBindTexture(GL_TEXTURE_2D, uv3);
        glActiveTexture(GL_TEXTURE2);
        glBindTexture(GL_TEXTURE_2D, uv2);
        glDrawArrays(GL_TRIANGLES, 0, 6);*/
        //the end of compute DIV and CURL///////////////////////////////////////////////
        

       


        //jacob iteration,to compute the p use uv1,bind uvfb,write into uv3.z //////////////////
        for (int i = 0; i < 170; i++) // 200 is fine
        {
            glBindFramebuffer(GL_FRAMEBUFFER, uvfb3);
            
            jacob.use();
            glActiveTexture(GL_TEXTURE1);
            glBindTexture(GL_TEXTURE_2D, uv2);
            glActiveTexture(GL_TEXTURE2);
            glBindTexture(GL_TEXTURE_2D, uv1);
            glDrawArrays(GL_TRIANGLES, 0, 6);
            // copy back
            /*glBindFramebuffer(GL_FRAMEBUFFER, uvfb1);
            pboundary.use();
            glActiveTexture(GL_TEXTURE0);
            glBindTexture(GL_TEXTURE_2D, uv3);
            glDrawArrays(GL_TRIANGLES, 0, 6);*/
            //another jacobi iteration
            glBindFramebuffer(GL_FRAMEBUFFER, uvfb1);
            jacob2.use();
            glActiveTexture(GL_TEXTURE1);
            glBindTexture(GL_TEXTURE_2D, uv2);
            glActiveTexture(GL_TEXTURE2);
            glBindTexture(GL_TEXTURE_2D, uv3);
            glDrawArrays(GL_TRIANGLES, 0, 6);
        }
        // after p jacob uv1 is just the same as  uv3 !!!!!!!!! jacob p 
        //THE END OF JOCAB PRESSURE        //////////////////////////////////////////////////////


        //gradient ///////////////////////////////////////////////////////////////////
        //after p jacob,uv0->enhance vort->uv1 uv1->subtract gradient->uv0
        glBindFramebuffer(GL_FRAMEBUFFER, uvfb3);
        gradient.use();
        glActiveTexture(GL_TEXTURE1);
        glBindTexture(GL_TEXTURE_2D, uv1);
        glDrawArrays(GL_TRIANGLES, 0, 6);

        //test gradient  show the gradient in uv1
       /*glBindFramebuffer(GL_FRAMEBUFFER, uvfb3);
       //glDrawBuffer(GL_COLOR_ATTACHMENT0);
       gradient2.use();
       glActiveTexture(GL_TEXTURE1);
       glBindTexture(GL_TEXTURE_2D, uv1);
       glDrawArrays(GL_TRIANGLES, 0, 6);*/
       //THE END OF SUBTRACT GRADIENT ///////////////////////////////////////////////


        //enhancement vorticity  ///////////////////////////////////////////////////////////////
       //if dont enhance,in this step just simply copy from uv3 to uv0
       glBindFramebuffer(GL_FRAMEBUFFER, uvfb0);
       vort.use();
       glActiveTexture(GL_TEXTURE2);
       glBindTexture(GL_TEXTURE_2D, uv3); // currenty set uv1 for test
       glActiveTexture(GL_TEXTURE1);
       glBindTexture(GL_TEXTURE_2D, uv2);
       glDrawArrays(GL_TRIANGLES, 0, 6);
       //THE END OF ENHANCE VORTICITY /////////////////////////////////////////////////////////

       

        glBindVertexArray(0);
        //the final output
        glBindFramebuffer(GL_FRAMEBUFFER, 0);
        screenShader.use();
        glBindVertexArray(quadVAO);
        glActiveTexture(GL_TEXTURE0);
        glBindTexture(GL_TEXTURE_2D, dye1);//dye1
        glDrawArrays(GL_TRIANGLES, 0, 6);

        glfwSwapBuffers(window);
        glfwPollEvents();
    }







}







void processInput(GLFWwindow* window)
{
    if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS)
        glfwSetWindowShouldClose(window, true);

    if (glfwGetKey(window, GLFW_KEY_W) == GLFW_PRESS)
        camera.ProcessKeyboard(FORWARD, deltaTime);
    if (glfwGetKey(window, GLFW_KEY_S) == GLFW_PRESS)
        camera.ProcessKeyboard(BACKWARD, deltaTime);
    if (glfwGetKey(window, GLFW_KEY_A) == GLFW_PRESS)
        camera.ProcessKeyboard(LEFT, deltaTime);
    if (glfwGetKey(window, GLFW_KEY_D) == GLFW_PRESS)
        camera.ProcessKeyboard(RIGHT, deltaTime);
}

// glfw: whenever the window size changed (by OS or user resize) this callback function executes
// ---------------------------------------------------------------------------------------------
void framebuffer_size_callback(GLFWwindow* window, int width, int height)
{
    // make sure the viewport matches the new window dimensions; note that width and 
    // height will be significantly larger than specified on retina displays.
    glViewport(0, 0, width, height);
}

// glfw: whenever the mouse moves, this callback is called
// -------------------------------------------------------
void mouse_callback(GLFWwindow* window, double xposIn, double yposIn)
{
    float xpos = static_cast<float>(xposIn);
    float ypos = static_cast<float>(yposIn);
    if (firstMouse)
    {
        lastX = xpos;
        lastY = ypos;
        firstMouse = false;
    }

    float xoffset = xpos - lastX;
    float yoffset = lastY - ypos; // reversed since y-coordinates go from bottom to top

    lastX = xpos;
    lastY = ypos;

    camera.ProcessMouseMovement(xoffset, yoffset);
}

// glfw: whenever the mouse scroll wheel scrolls, this callback is called
// ----------------------------------------------------------------------
void scroll_callback(GLFWwindow* window, double xoffset, double yoffset)
{
    camera.ProcessMouseScroll(static_cast<float>(yoffset));
}

// utility function for loading a 2D texture from file
// ---------------------------------------------------
unsigned int loadTexture(char const* path)
{
    unsigned int textureID;
    glGenTextures(1, &textureID);

    int width, height, nrComponents;
    unsigned char* data = stbi_load(path, &width, &height, &nrComponents, 0);
    if (data)
    {
        GLenum format;
        if (nrComponents == 1)
            format = GL_RED;
        else if (nrComponents == 3)
            format = GL_RGB;
        else if (nrComponents == 4)
            format = GL_RGBA;

        glBindTexture(GL_TEXTURE_2D, textureID);
        glTexImage2D(GL_TEXTURE_2D, 0, format, width, height, 0, format, GL_UNSIGNED_BYTE, data);
        glGenerateMipmap(GL_TEXTURE_2D);

        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, format == GL_RGBA ? GL_CLAMP_TO_EDGE : GL_REPEAT); // for this tutorial: use GL_CLAMP_TO_EDGE to prevent semi-transparent borders. Due to interpolation it takes texels from next repeat 
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, format == GL_RGBA ? GL_CLAMP_TO_EDGE : GL_REPEAT);
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

        stbi_image_free(data);
    }
    else
    {
        std::cout << "Texture failed to load at path: " << path << std::endl;
        stbi_image_free(data);
    }

    return textureID;
}